punchy
======

.net support to control PunchLight Recording Light http://www.punchlight.com/?q=recording_lamp_usb

Special thanks to PunchLight and Pavel Kučera for providing details on how to connect and send commands to the PunchLight.

How To Use
==========

The API is extremely small and simple to use.

To get all the lights detected on the system call:

```
var lightList = Punchy.API.GetLights();
```

Once you have a light object you can save colors into one of the three colorslots, turn the light on/off, and set the brightness. Note that if you turn on the 'Flash' color slot the light will switch colors for about 2 seconds and then return to it's previous state (either off or one of the other two colors).

When you save a color to a slot the light will briefly change color as a preview of the color you saved.

