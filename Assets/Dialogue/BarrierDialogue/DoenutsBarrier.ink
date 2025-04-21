INCLUDE ../Global.ink

{ current_story_point:

    - 2:
        -> post_doenuts
    - else:
        If you can read this, something has gone terribly wrong
        -> END
}

====post_doenuts====
~ ChangeSpeaker("SCOUT")
Hey! What are you doing?
~ PlayCutscene("CENTRAL_BarrierAvoidSouth")
We got everything we needed from Doenuts. Let's get to the party!
-> END