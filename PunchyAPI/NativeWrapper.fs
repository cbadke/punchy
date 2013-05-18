namespace Punchy.Native
    module internal Wrapper =

        open System
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

