INCLUDE ../Global.ink

{ current_story_point:

    - 1:
        -> pre_doenuts
    - else:
        If you can read this, I broke something
        -> END
}

====pre_doenuts====
Where are you going?
~ PlayCutscene("BarrierAvoidRight")
You can’t be tired already! You just got out of bed!
-> END