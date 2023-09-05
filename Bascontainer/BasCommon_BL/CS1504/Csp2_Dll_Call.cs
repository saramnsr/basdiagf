using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace CS1504
{
    class Csp2_Dll_Call
    {

        // Returned status values...
        public const int STATUS_OK = 0;
        public const int COMMUNICATIONS_ERROR = -1;
        public const int BAD_PARAM = -2;
        public const int SETUP_ERROR = -3;
        public const int INVALID_COMMAND_NUMBER = -4;
        public const int COMMAND_LRC_ERROR = -7;
        public const int RECEIVED_CHARACTER_ERROR = -8;
        public const int GENERAL_ERROR = -9;
        public const int FILE_NOT_FOUND = 2;
        public const int ACCESS_DENIED = 5;

        // Parameter values...
        public const int PARAM_OFF = 0;
        public const int PARAM_ON = 1;

        public const int DETERMINE_SIZE = 0;

        public const int COM1 = 0;
        public const int COM2 = 1;
        public const int COM3 = 2;
        public const int COM4 = 3;
        public const int COM5 = 4;
        public const int COM6 = 5;
        public const int COM7 = 6;
        public const int COM8 = 7;
        public const int COM9 = 8;
        public const int COM10 = 9;
        public const int COM11 = 10;
        public const int COM12 = 11;
        public const int COM13 = 12;
        public const int COM14 = 13;
        public const int COM15 = 14;
        public const int COM16 = 15;



        // Communications
        [DllImport("Csp2.dll")]
        public static extern int csp2Init(int nComPort);
        [DllImport("Csp2.dll")]
        public static extern int csp2Restore();
        [DllImport("Csp2.dll")]
        public static extern int csp2WakeUp();
        [DllImport("Csp2.dll")]
        public static extern int csp2DataAvailable();

        // Basic Functions
        [DllImport("Csp2.dll")]
        public static extern int csp2ReadData();
        [DllImport("Csp2.dll")]
        public static extern int csp2ClearData();
        [DllImport("Csp2.dll")]
        public static extern int csp2PowerDown();
        [DllImport("Csp2.dll")]
        public static extern int csp2GetTime(System.Text.StringBuilder aTimeBuf);
        [DllImport("Csp2.dll")]
        public static extern int csp2SetTime(char[] aTimeBuf);
        [DllImport("Csp2.dll")]
        public static extern int csp2SetDefaults();

        // CSP Data Get
        [DllImport("Csp2.dll")]
        public static extern int csp2GetPacket(System.Text.StringBuilder szBarData, int nBarcodeNumber, int nMaxLength);
        [DllImport("Csp2.dll")]
        public static extern int csp2GetDeviceId(System.Text.StringBuilder szDeviceId, int nMaxLength);
        [DllImport("Csp2.dll")]
        public static extern int csp2GetProtocol();
        [DllImport("Csp2.dll")]
        public static extern int csp2GetSystemStatus();
        [DllImport("Csp2.dll")]
        public static extern int csp2GetSwVersion(System.Text.StringBuilder szSwVersion, int nMaxLength);
        [DllImport("Csp2.dll")]
        public static extern int csp2GetASCIIMode();
        [DllImport("Csp2.dll")]
        public static extern int csp2GetRTCMode();


        // DLL Configuration
        [DllImport("Csp2.dll")]
        public static extern int csp2SetRetryCount(int nRetryCount);
        [DllImport("Csp2.dll")]
        public static extern int csp2GetRetryCount();


        // Miscellaneous
        [DllImport("Csp2.dll", CharSet = CharSet.Ansi)]
        public static extern int csp2GetDllVersion(System.Text.StringBuilder szDllVersion, int nMaxLength);
        [DllImport("Csp2.dll")]
        public static extern int csp2TimeStamp2Str(System.Text.StringBuilder Stamp, System.Text.StringBuilder value, int nMaxLength);
        [DllImport("Csp2.dll")]
        public static extern int csp2GetCodeType(int CodeID, System.Text.StringBuilder CodeType, int nMaxLength);

        // Advanced functions
        [DllImport("Csp2.dll")]
        public static extern int csp2ReadRawData(System.Text.StringBuilder aBuffer, int nMaxLength);
        [DllImport("Csp2.dll")]
        public static extern int csp2SetParam(int nParam, System.Text.StringBuilder szString, int nMaxLength);
        [DllImport("Csp2.dll")]
        public static extern int csp2GetParam(int nParam, System.Text.StringBuilder szString, int nMaxLength);
        [DllImport("Csp2.dll")]
        public static extern int csp2Interrogate();
        [DllImport("Csp2.dll")]
        public static extern int csp2GetCTS();
        [DllImport("Csp2.dll")]
        public static extern int csp2SetDTR(int nOnOff);
        [DllImport("Csp2.dll")]
        public static extern int csp2SetDebugMode(int nOnOff);
        //[DllImport("Csp2.dll")]
        //public static extern int csp2StartPolling(FARPROC csp2CallBack);
        [DllImport("Csp2.dll")]
        public static extern int csp2StopPolling();
    }
}


/*
 /*******************************************************************************
*                      
*  Filename:      K:\keyfob\tony\winfob\Csp2.h
*
*  Copyright(c) Symbol Technologies Inc., 2001
*  
*  Description:     This file provides the user API interface
*                   function prototypes for Symbol's CS1504 Consumer Scanning 
*                   Products as Dynamic Link Library. When 
*                   compiled into a DLL, the user can access 
*                   all of the functions available for the 
*                   Symbol CS1504.
*
*  Author:          Tony Russo
*
*  Creation Date:   4/04/2001
*
*  Derived From:    New File
*
*  Edit History:
*   $Log:   U:/keyfob/archives/winfob/examples/msvc/Csp2.h_V  $
 * 
 *    Rev 1.0   Jan 22 2002 09:52:36   pangr
 * Initial revision.
* 
*    Rev 1.0   05 Apr 2001 09:24:16   RUSSOA
* Initial revision.
#ifdef DLL_IMPORT_EXPORT
    #undef DLL_IMPORT_EXPORT
#endif

#ifdef DLL_SOURCE_CODE
    #define DLL_IMPORT_EXPORT __declspec(dllexport) __stdcall
#else
    #define DLL_IMPORT_EXPORT __declspec(dllimport) __stdcall
#endif

#ifdef __cplusplus
    #define NoMangle extern "C"
#else
    #define NoMangle
#endif

// Returned status values...
#define STATUS_OK                   ((int) 0)
#define COMMUNICATIONS_ERROR        ((int)-1)  
#define BAD_PARAM                   ((int)-2)
#define SETUP_ERROR                 ((int)-3)
#define INVALID_COMMAND_NUMBER      ((int)-4)  
#define COMMAND_LRC_ERROR           ((int)-7)  
#define RECEIVED_CHARACTER_ERROR    ((int)-8)  
#define GENERAL_ERROR               ((int)-9)  
#define FILE_NOT_FOUND              ((int) 2)
#define ACCESS_DENIED               ((int) 5)

// Parameter values...
#define PARAM_OFF                   ((int) 0) 
#define PARAM_ON                    ((int) 1) 

#define DETERMINE_SIZE              ((int) 0)


#ifndef COM1
    #define COM1                    ((int) 0)
    #define COM2                    ((int) 1)
    #define COM3                    ((int) 2)
    #define COM4                    ((int) 3)
    #define COM5                    ((int) 4)
    #define COM6                    ((int) 5)
    #define COM7                    ((int) 6)
    #define COM8                    ((int) 7)
    #define COM9                    ((int) 8)
    #define COM10                   ((int) 9)
    #define COM11                   ((int)10)
    #define COM12                   ((int)11)
    #define COM13                   ((int)12)
    #define COM14                   ((int)13)
    #define COM15                   ((int)14)
    #define COM16                   ((int)15)
#endif

// Communications
NoMangle int DLL_IMPORT_EXPORT csp2Init(int nComPort);
NoMangle int DLL_IMPORT_EXPORT csp2Restore(void);
NoMangle int DLL_IMPORT_EXPORT csp2WakeUp(void);
NoMangle int DLL_IMPORT_EXPORT csp2DataAvailable(void);

// Basic Functions
NoMangle int DLL_IMPORT_EXPORT csp2ReadData(void);
NoMangle int DLL_IMPORT_EXPORT csp2ClearData(void);
NoMangle int DLL_IMPORT_EXPORT csp2PowerDown(void);
NoMangle int DLL_IMPORT_EXPORT csp2GetTime(unsigned char aTimeBuf[]);
NoMangle int DLL_IMPORT_EXPORT csp2SetTime(unsigned char aTimeBuf[]);
NoMangle int DLL_IMPORT_EXPORT csp2SetDefaults(void);

// CSP Data Get
NoMangle int DLL_IMPORT_EXPORT csp2GetPacket(char szBarData[], int nBarcodeNumber, int nMaxLength);
NoMangle int DLL_IMPORT_EXPORT csp2GetDeviceId(char szDeviceId[9], int nMaxLength);
NoMangle int DLL_IMPORT_EXPORT csp2GetProtocol(void);
NoMangle int DLL_IMPORT_EXPORT csp2GetSystemStatus(void);
NoMangle int DLL_IMPORT_EXPORT csp2GetSwVersion(char szSwVersion[9], int nMaxLength);
NoMangle int DLL_IMPORT_EXPORT csp2GetASCIIMode(void);
NoMangle int DLL_IMPORT_EXPORT csp2GetRTCMode(void);

// DLL Configuration
NoMangle int DLL_IMPORT_EXPORT csp2SetRetryCount(int nRetryCount);
NoMangle int DLL_IMPORT_EXPORT csp2GetRetryCount(void);

// Miscellaneous
NoMangle int DLL_IMPORT_EXPORT csp2GetDllVersion(char szDllVersion[], int nMaxLength);
NoMangle int DLL_IMPORT_EXPORT csp2TimeStamp2Str(unsigned char *Stamp, char *value, int nMaxLength);
NoMangle int DLL_IMPORT_EXPORT csp2GetCodeType(unsigned int CodeID, char *CodeType, int nMaxLength);

// Advanced functions
NoMangle int DLL_IMPORT_EXPORT csp2ReadRawData(char aBuffer[], int nMaxLength);
NoMangle int DLL_IMPORT_EXPORT csp2SetParam(int nParam, char szString[], int nMaxLength);
NoMangle int DLL_IMPORT_EXPORT csp2GetParam(int nParam, char szString[], int nMaxLength);
NoMangle int DLL_IMPORT_EXPORT csp2Interrogate(void);
NoMangle int DLL_IMPORT_EXPORT csp2GetCTS(void);
NoMangle int DLL_IMPORT_EXPORT csp2SetDTR(int nOnOff);
NoMangle int DLL_IMPORT_EXPORT csp2SetDebugMode(int nOnOff);
NoMangle int DLL_IMPORT_EXPORT csp2StartPolling(FARPROC csp2CallBack);
NoMangle int DLL_IMPORT_EXPORT csp2StopPolling(void);


 
 
 */