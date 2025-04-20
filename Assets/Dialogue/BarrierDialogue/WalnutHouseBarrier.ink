INCLUDE ../Global.ink

{ current_story_point:

    - 1:
        -> pre_doenuts
    - 4:
        -> post_murder
    - else:
        If you can read this, something has gone terribly wrong
        -> END
}

====pre_doenuts====
~ ChangeSpeaker("SCOUT")
Ah! Wait!
~ PlayCutscene("BarrierAvoidSouth")
We should probably wait to go into Wally’s house until after we get the cake.
-> END

====post_murder====
~ ChangeSpeaker("SCOUT")
Meanie! What the heck are you doing?!
~ PlayCutscene("BarrierAvoidSouth")
Are you insane?!
-> END