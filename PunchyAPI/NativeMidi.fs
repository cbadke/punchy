namespace Punchy.Native

    module internal Midi =

        open System
        open System.Runtime.InteropServices
        open Microsoft.FSharp.NativeInterop

        type MMRESULT =
            | Success = 0
            | Error = 1
            | BadDeviceId = 2
            | NotEnabled = 3
            | Allocated = 4
            | InvalidHandle = 5
            | NoDriver = 6
            | NoMemory = 7
            | NotSupported = 8
            | BadErrorNumber = 9
            | InvalidFlag = 10
            | InvalidParameter = 11
            | HandleBudy = 12
            | InvalidAlias = 13
            | BadDb = 14
            | KeyNotFound = 15
            | ReadError = 16
            | WriteError = 17
            | DeleteError = 18
            | ValueNotFound = 19
            | NoDriverCb= 20
            | BadWaveFormat = 32
            | WaveStillPlaying = 33
            | WaveUnprepared = 34

        [<StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)>]
        type MIDIOUTCAPS =
            struct
                val mutable wMid : UInt16
                val mutable wPid : UInt16
                val mutable vDriverVersion : UInt32
                [<MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)>]
                val mutable szPname : String
                val mutable wTechnology : UInt16
                val mutable wVoices : UInt16
                val mutable wNotes : UInt16
                val mutable wChannelMask : UInt16
                val mutable dwSupport : UInt32
            end
        let MIDIOUTCAPS_SIZE = uint32 (Marshal.SizeOf(typeof<MIDIOUTCAPS>))

        [<StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)>]
        type MIDIHDR =
            struct
                val mutable lpData : IntPtr
                val mutable dwBufferLength : UInt32
                val mutable dwBytesRecorded : UInt32
                val mutable dwUser : IntPtr
                val mutable dwFlags : UInt32
                val mutable lpNext : IntPtr
                val mutable reserved : IntPtr
                val mutable dwOffSet : UInt32

                [<MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)>]
                val mutable dwReserved : byte array
            end
        let MIDIHDR_SIZE = 48u

        type HMIDIOUT = IntPtr

        [<DllImport(@"winmm.dll", EntryPoint="midiOutGetNumDevs")>]
        extern UInt32 midiOutGetNumDevs()

        [<DllImport(@"winmm.dll", EntryPoint="midiOutGetDevCaps")>]
        extern MMRESULT midiOutGetDevCaps(
            UIntPtr uDeviceID,      
            MIDIOUTCAPS& lpMidiOutCaps,  
            UInt32 cbMidiOutCaps);

        [<DllImport(@"winmm.dll", EntryPoint="midiOutOpen")>]
        extern MMRESULT midiOutOpen(
            HMIDIOUT& lphmo,
            UInt32 uDeviceID,
            IntPtr dwCallback,
            IntPtr dwCallbackInstance,
            UInt32 dwFlags);

        [<DllImport(@"winmm.dll", EntryPoint="midiOutClose")>]
        extern MMRESULT midiOutClose(
            HMIDIOUT hmo);

        [<DllImport(@"winmm.dll", EntryPoint="midiOutPrepareHeader")>]
        extern MMRESULT midiOutPrepareHeader(
            HMIDIOUT hmo,
            MIDIHDR& lpMidiOutHdr,
            UInt32 cbMidiOutHdr);

        [<DllImport(@"winmm.dll", EntryPoint="midiOutUnprepareHeader")>]
        extern MMRESULT midiOutUnprepareHeader(
            HMIDIOUT hmo,
            MIDIHDR& lpMidiOutHdr,
            UInt32 cbMidiOutHdr);

        [<DllImport(@"winmm.dll", EntryPoint="midiOutLongMsg")>]
        extern MMRESULT midiOutLongMsg(
            HMIDIOUT hmo,
            MIDIHDR& lpMidiOutHdr,
            UInt32 cbMidiOutHdr);
