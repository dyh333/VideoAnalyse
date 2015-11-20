using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace NVRCsharpDemo
{
    class MODSDK
    {
        public MODSDK()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        /*************************************************
        参数配置结构、参数
        **************************************************/
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct POINT
        {
            public int x;
            public int y;
        }

        //typedef struct tagJTPOLYGON_INFO
        //{
        //    WORD wType;//区域类型，0：表示处理区域，1：表示最小检测目标大小区域。
        //               //2：表示消失线，共4个点组成，前面2个点表示一条线，后面2个点表示另外一条线
        //               //3:表示虚拟门禁，只有一条线
        //    WORD wPointCount;//区域点数
        //    POINT* pPoints;//每个点的坐标
        //}
        //JTPOLYGON_INFO,*PJTPOLYGON_INFO;
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct JTPOLYGON_INFO
        {
            public ushort wType;//区域类型，0：表示处理区域，1：表示最小检测目标大小区域。
                              //2：表示消失线，共4个点组成，前面2个点表示一条线，后面2个点表示另外一条线	
            public ushort wPointCount;//区域点数
            public IntPtr pPoints;//每个点的坐标
        }

        //        typedef struct tagJtMOD_OBJECTPARAM
        //        {
        //            WORD left;//目标位置左
        //            WORD top;//目标位置顶
        //            WORD right;//目标位置右
        //            WORD bottom;//目标位置底
        //            WORD actionDirector;//目标行进方向
        //            DWORD objectID; //目标ID号
        //            DWORD realOjbectID;//按序增长的目标ID号
        //            float objectspeed;//目标当前速度
        //            float grayhist[8];//灰度直方图概率密度
        //            DWORD dwExtLen;//扩展结构长度 0

        //            WORD objectCenterPointX;//目标质心X
        //            WORD objectCenterPointY;//目标质心Y
        //            WORD objectPosVsLine;  //目标质心点相对于虚拟门禁线的位置，0：下方；  1：上方

        //            struct tagJtMOD_OBJECTPARAM* next;//指向下一个目标
        //        }JtMOD_OBJECTPARAM;
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct JtMOD_OBJECTPARAM
        {
            public ushort left;//目标位置左
            public ushort top;//目标位置顶
            public ushort right;//目标位置右
            public ushort bottom;//目标位置底
            public ushort actionDirector;//目标行进方向
            public uint objectID; //目标ID号
            public uint realOjbectID;//按序增长的目标ID号
            public float objectspeed;//目标当前速度
            [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.R4, SizeConst = 8)]
            public float[] grayhist;//灰度直方图概率密度
            public uint dwExtLen;//扩展结构长度 0

            public ushort objectCenterPointX;//目标质心X
            public ushort objectCenterPointY;//目标质心Y
            public ushort objectPosVsLine;  //目标质心点相对于虚拟门禁线的位置，0：下方；  1：上方

            public IntPtr next;//指向下一个目标
        }

        //typedef struct tagJTMOD_CALLBACK_PARAM
        //{
        //    WORD size;//本结构体的长度	
        //    WORD nWidth;//处理图像宽度
        //    WORD nHeight;//处理图像高度
        //    WORD Datatype;//图像数据类型（0：表示YV12格式，1：表示I420格式）
        //    DWORD lDataLen;//图像数据长度
        //    DWORD timestamp;//时间戳//ms

        //    WORD entryNum;//进入的人数   
        //    WORD exitNum;//出去的人数
        //    WORD ojbectNumOfFrame;//当前帧包含目标的个数

        //    void* pData;//目标数据

        //    JtMOD_OBJECTPARAM* pobject;//目标指针链表

        //    DWORD dwReverse[1];//保留字
        //}
        //JTMOD_CALLBACK_PARAM,*PJTMOD_CALLBACK_PARAM;
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct JTMOD_CALLBACK_PARAM
        {
            public ushort size;//本结构体的长度	
            public ushort nWidth;//处理图像宽度
            public ushort nHeight;//处理图像高度
            public ushort Datatype;//图像数据类型（0：表示YV12格式，1：表示I420格式）
            public uint lDataLen;//图像数据长度
            public uint timestamp;//时间戳//ms
            [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.ByValTStr, SizeConst = 15)]
            public char[] cameraIP;//相机IP字符数组
            public ushort nChannelID;//通道号

            public ushort entryNum;//进入的人数   
            public ushort exitNum;//出去的人数
            public ushort ojbectNumOfFrame;//当前帧包含目标的个数

            public IntPtr pData;//目标数据

            public IntPtr pobject;//目标指针链表

            [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.SysUInt, SizeConst = 1)]
            public uint[] dwReverse; //保留字
        }

        ////回调函数原型
        //typedef void (__stdcall* PCMP_FRAME_INFO)(DWORD,PJTMOD_CALLBACK_PARAM);
        public delegate void CMP_FRAME_INFO_DELEGATE(uint aaa, IntPtr callBackParam);

        //typedef struct tagJTMOD_OPEN_PARAM
        //{
        //    WORD size;//本结构体的长度
        //    WORD nWidth;//处理图像宽度
        //    WORD nHeight;//处理图像高度	
        //    WORD wPolygons;//区域个数
        //    PJTPOLYGON_INFO pPolygons;//区域信息

        //    DWORD startobjectID;//开始目标ID

        //    PCMP_FRAME_INFO pCallback;//回调函数
        //    DWORD dwCallback;//回调参数

        //    DWORD dwReverse[1];//保留字
        //}JTMOD_OPEN_PARAM
        public struct JTMOD_OPEN_PARAM
        {
            public ushort size;//本结构体的长度
            public ushort nWidth;//处理图像宽度
            public ushort nHeight;//处理图像高度	
            public ushort wPolygons;//区域个数
            public IntPtr pPolygons;//区域信息

            public uint startobjectID;//开始目标ID 

            [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.ByValTStr, SizeConst = 15)]
            public char[] cameraIP;//相机IP字符数组
            public uint nChannelID;//通道ID
            public uint calibrationObjectWidth;//用户标定的目标像素宽度
            public uint calibrationObjectHeight;//用户标定的目标像素高度

            public CMP_FRAME_INFO_DELEGATE pCallback;//回调函数
            public uint dwCallback;//回调参数

            [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.SysUInt, SizeConst = 1)]
            public uint[] dwReverse; //保留字
        }

        //typedef struct tagJTMOD_FRAME_INFO
        //{
        //    WORD size;//本结构体的长度
        //    WORD nWidth;//图像宽度
        //    WORD nHeight;//图像高度
        //    WORD Datatype;//YUV数据类型（0：表示YV12格式，1：表示I420格式）
        //    DWORD lDataLen;//图像数据长度	
        //    DWORD iFrameIndex;//图像帧号
        //    DWORD timestamp;//时间戳//ms
        //    void* pData;//图像数据指针
        //    DWORD ImportGrade;//重要等级
        //    DWORD dwReverse[1];//保留字
        //}JTMOD_FRAME_INFO,*PJTMOD_FRAME_INFO;
        public struct JTMOD_FRAME_INFO
        {
            public ushort size;//本结构体的长度
            public ushort nWidth;//处理图像宽度
            public ushort nHeight;//处理图像高度	
            public ushort datatype;//区域个数
            public uint lDataLen;//图像数据长度	
            public uint iFrameIndex;//图像帧号
            public uint timestamp;//时间戳//ms
            public IntPtr pData;//图像数据指针
            public uint importGrade;//重要等级
            [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.SysUInt, SizeConst = 1)]
            public uint[] dwReverse; //保留字
        }








        /********************************SDK接口函数声明*********************************/

        [DllImport(@"..\bin\MODdll.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool JtModTest(ref JTPOLYGON_INFO JtPolygonInfo);


        /******************************************************************
         函数名：JtModCreate
         参数：pAlgOpenParam：初始化参数
         返回值：返回算法分析ID号，如果返回0表示创建失败
         功能描述：创建运动目标检测算法
              
         ******************************************************************/
        [DllImport(@"..\bin\MODdll.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int JtModCreate(ref JTMOD_OPEN_PARAM algOpenParam);


        /******************************************************************
         函数名：JtModProcess
         参数：ModID：算法分析ID号
	           pFrameInfo：每帧数据参数
         返回值：0：算法分析成功；
		         1：算法处理来不及；
		         -1：算法处理出错，请关闭算法后重新创建；
         功能描述：
              
         ******************************************************************/
        [DllImport(@"..\bin\MODdll.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int JtModProcess(int ModID, ref JTMOD_FRAME_INFO frameInfo);
    }
}
