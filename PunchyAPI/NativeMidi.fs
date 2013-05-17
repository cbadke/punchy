module private NativeMidi

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


    [<DllImport(@"winmm.dll", EntryPoint="midiOutGetNumDevs")>]
    extern UInt32 midiOutGetNumDevs()

    [<DllImport(@"winmm.dll", EntryPoint="midiOutGetDevCaps")>]
    extern MMRESULT midiOutGetDevCaps(
        UIntPtr uDeviceID,      
        MIDIOUTCAPS& lpMidiOutCaps,  
        UInt32 cbMidiOutCaps);

    [<DllImport(@"winmm.dll", EntryPoint="midiOutOpen")>]
    extern MMRESULT midiOutOpen(
        IntPtr& lphmo,
        UInt32 uDeviceID,
        IntPtr dwCallback,
        IntPtr dwCallbackInstance,
        UInt32 dwFlags);

    [<DllImport(@"winmm.dll", EntryPoint="midiOutClose")>]
    extern MMRESULT midiOutClose(
        IntPtr hmo);
