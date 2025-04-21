INCLUDE ../Global.ink

{ current_story_point:

    - 3:
        -> party
    - else:
        If you can read this, something has gone terribly wrong
        -> END
}
==== party ====
~ChangeSpeaker("GINGER")
Hey, you can’t leave!
~ PlayCutscene("CENTRAL_BarrierAvoidNorth")
You still have the cake!
-> END