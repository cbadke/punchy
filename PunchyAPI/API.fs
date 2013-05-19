namespace Punchy
    module API =

        type Light(id) =
            member this.TurnOn colorSlot =
                let data = CommandBuilder.ActivateLight colorSlot
                Native.SendData data id

            member this.TurnOff() =
                let data = CommandBuilder.DeactivateLight()
                Native.SendData data id

            member this.SaveColor color colorSlot =
                let red = CommandBuilder.SaveRedColor color colorSlot
                let green = CommandBuilder.SaveGreenColor color colorSlot
                let blue = CommandBuilder.SaveBlueColor color colorSlot
                let r = Native.SendData red id
                let g = Native.SendData green id
                let b = Native.SendData blue id
                r && g && b

            member this.SetBrightness brightness =
                let data = CommandBuilder.SetBrightness brightness
                Native.SendData data id

        let GetLights() =
            Native.AvailableDevices() |> List.map (fun x -> new Light(x))