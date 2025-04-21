INCLUDE ../Global.ink

{ current_story_point:

    - 1:
        -> pre_doenuts
    - 2:
        -> post_doenuts
    - else:
        If you can read this, something has gone terribly wrong
        -> END
}
-> END
====pre_doenuts====
~ ChangeSpeaker("SCOUT")
Where are you going?
~ PlayCutscene("CENTRAL_BarrierAvoidEast")
There’s nothing but woods over here.
Last I checked, Doenuts is the other way.
-> END

====post_doenuts====
~ ChangeSpeaker("SCOUT")
Meanie, where are you going?
~ PlayCutscene("CENTRAL_BarrierAvoidEast")
The only thing out there is the forest. No Walnuts.
-> END