INCLUDE ../Global.ink

{ current_story_point:

    - 1:
        -> pre_doenuts
    - 2:
        -> post_doenuts
    - 4:
        -> post_murder
    - else:
        If you can read this, I broke something
        -> END
}

====pre_doenuts====
Where are you going?
~ PlayCutscene("BarrierAvoidSouth")
You can’t be tired already! You just got out of bed!
-> END

====post_doenuts====
Um... Meanie?
~ PlayCutscene("BarrierAvoidSouth")
That's the wrong house... Come on, stop playing around.
-> END

====post_murder====
Meanie... stop...
~ PlayCutscene("BarrierAvoidSouth")
There’s no going back, Meanie.
-> END