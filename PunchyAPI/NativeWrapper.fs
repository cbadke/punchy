namespace Punchy.Native
    module internal Wrapper =

        open System
        open System.Runtime.InteropServices
        open Microsoft.FSharp.NativeInterop
        open Midi

        let Devices() =
            let Connectable id =
                let mutable handle = IntPtr.Zero
                match midiOutOpen(&handle, id, IntPtr.Zero, IntPtr.Zero, 0u) with
                | MMRESULT.Success -> 
                    midiOutClose(handle) |> ignore
                    true
                | _ -> false
            let deviceCount = midiOutGetNumDevs()
            let names = seq {
                for i in 1u .. deviceCount do
                    let mutable caps = new MIDIOUTCAPS()
                    match midiOutGetDevCaps(UIntPtr (i-1u), &caps, MIDIOUTCAPS_SIZE) with
                    | MMRESULT.Success -> yield (i-1u, caps.szPname)
                    | _ -> ();
            }
            names |> Seq.filter (fun x -> Connectable (fst x)) |> Seq.toList

        let SendOn id =
            let mutable handle = IntPtr.Zero
            match midiOutOpen(&handle, id, IntPtr.Zero, IntPtr.Zero, 0u) with
            | MMRESULT.Success ->
                let data = [| 0xF0uy; 0x7Euy; 0x00uy; 
                              0x07uy; 0x02uy; 0x01uy; 
                              0x00uy; 0x03uy; 0x08uy; 
                              0x02uy; 0xF7uy; |]
                let mutable packet = new MIDIHDR()
                let dp = Marshal.AllocCoTaskMem(Array.length data)

                for i in 0 .. ((Array.length data) - 1) do
                    let t = IntPtr(dp.ToInt32() + i)
                    Marshal.StructureToPtr(data.[i], t, false)

                packet.lpData <- IntPtr (dp.ToPointer())
                packet.dwBufferLength <- uint32 (Array.length data)
                packet.dwBytesRecorded <- uint32 (Array.length data)
                packet.dwUser <- IntPtr.Zero

                printfn "%A" packet.dwFlags
                midiOutPrepareHeader(handle, &packet, MIDIHDR_SIZE) |> printfn "%A"
                printfn "%A" packet.dwFlags
                midiOutLongMsg(handle, &packet, MIDIHDR_SIZE) |> printfn "%A"
                printfn "%A" packet.dwFlags
                midiOutUnprepareHeader(handle, &packet, MIDIHDR_SIZE) |> printfn "%A"
                printfn "%A" packet.dwFlags

                Marshal.FreeCoTaskMem(dp)

                midiOutClose(handle) |> ignore
                true
            | _ -> false
