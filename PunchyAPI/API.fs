namespace Punchy
    module API =

        type Light(id) =
            member this.TurnOn slot =
                let data = CommandBuilder.ActivateLight slot
                Native.SendData data id

            member this.TurnOff() =
                let data = CommandBuilder.DeactivateLight()
                Native.SendData data id

        let GetLights() =
            Native.AvailableDevices() |> List.map (fun x -> new Light(x))