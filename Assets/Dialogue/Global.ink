EXTERNAL ChangeSpeaker(name)
EXTERNAL PlayCutscene(cutscene)
EXTERNAL ChangeStoryPoint(storypoint)
EXTERNAL UpdateDialogueState(state)

VAR cake_flavor = ""
VAR dialogue_state = 0
VAR current_story_point = 0

=== function ChangeSpeaker(name) ===
// Placeholder
~ return 1

=== function PlayCutscene(cutsceneIndex) ===
// Placeholder
~ return 1

=== function ChangeStoryPoint(storypointIndex) ===
~ current_story_point = storypointIndex

=== function UpdateDialogueState(state)===
~ return 1