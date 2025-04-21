INCLUDE ../Global.ink

{ current_story_point:

    - 4:
        -> post_murder
    - else:
        If you can read this, something has gone terribly wrong
        -> END
}

====post_murder====
Where are you going?!
~ PlayCutscene("CENTRAL_BarrierAvoidWest")
We've got to get out of here!
-> END