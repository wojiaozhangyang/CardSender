using MachineBox.Core.Enums;
using MachineBox.Core.Models;
using System;
using System.Configuration;
using System.Runtime.InteropServices;
namespace MachineBox.Core.CardReaders
{
    public class CRT602UReader
    {

        #region |   P/Invoke for Mifare Reader   |

        [DllImport("CRT_602_U.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern Int32 CommOpen();

        [DllImport("CRT_602_U.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        private static extern Int32 CommClose(UInt32 Handle);

        [DllImport("CRT_602_U.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        private static extern Int32 GetDeviceCapabilities(UInt32 Handle, ref Int16 InputReportByteLength, ref Int16 OutputReportByteLength);

        [DllImport("CRT_602_U.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        private static extern Int32 CRT602U_BeepOn(UInt32 Handle);

        [DllImport("CRT_602_U.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        private static extern Int32 CRT602U_BeepOff(UInt32 Handle);

        [DllImport("CRT_602_U.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern Int32 CRT602U_Readsn(UInt32 Handle, ref byte SNDATA, ref byte SNDATALen);

        [DllImport("CRT_602_U.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        private static extern Int32 RF_DetectCard(UInt32 Handle);

        [DllImport("CRT_602_U.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        private static extern Int32 RF_GetCardID(UInt32 Handle, ref byte GeRfIDLen, ref byte GeRfID);

        #endregion

        private UInt32 _handCom;
        private Int16  _inputReportByteLength;
        private Int16  _outputReportByteLength;

        /// <summary>
        /// 
        /// </summary>
        public CardReaderResponse IsConnected()
        {
            var response = new CardReaderResponse { Status = ResponseStatuses.FAILURE };

            int handCom = default(int);

            try
            {
                handCom = CommOpen();

                response.Status = handCom != 0 ? ResponseStatuses.SUCCESS : ResponseStatuses.FAILURE;
            }
            catch
            {
                response.Status = ResponseStatuses.FAILURE;
            }
            finally
            {
                CommClose(_handCom);
            }

            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public CardReaderResponse Read()
        {
            var response = new CardReaderResponse { Status = ResponseStatuses.FAILURE };

            try
            {
                if ((_handCom = (UInt32)CommOpen()) != 0)
                {
                    if (GetDeviceCapabilities(_handCom, ref _inputReportByteLength, ref _outputReportByteLength) == 0)
                    {
                        DateTime start = DateTime.Now;

                        while (true)
                        {
                            if ((DateTime.Now - start).TotalSeconds >= int.Parse(ConfigurationManager.AppSettings["readTimeout"]))
                            {
                                response.Status = ResponseStatuses.TIMEOUT_EXPIRED;
                                break;
                            }

                            if (RF_DetectCard(_handCom) == 0)
                            { 
                                byte   geRfIDLen = 4;
                                byte[] geRfID    = new byte[4];
                                string strBuf    = string.Empty;

                                if (RF_GetCardID(_handCom, ref geRfIDLen, ref geRfID[0]) == 0)
                                {
                                    CRT602U_BeepOn(_handCom);

                                    for (int i = 0; i < 4; i++)
                                        strBuf += geRfID[i] < 16 ? ("0" + geRfID[i].ToString("X2")) : geRfID[i].ToString("X2");
                                       
                                    CRT602U_BeepOff(_handCom);

                                    response.Status = ResponseStatuses.SUCCESS;
                                    response.Data = strBuf;

                                    break;
                                }
                            }
                        }
                    }
                    else
                        response.Status = ResponseStatuses.FAILURE;
                }
                else
                    response.Status = ResponseStatuses.DEVICE_NOT_FOUND;
            }
            catch
            {
                response.Status = ResponseStatuses.FAILURE;
            }
            finally
            {
                try
                {
                    if (_handCom != 0)
                        CommClose(_handCom);
                }
                catch
                {
                    response.Status = ResponseStatuses.FAILURE;
                }
            }

            return response;
        }
    }
}
