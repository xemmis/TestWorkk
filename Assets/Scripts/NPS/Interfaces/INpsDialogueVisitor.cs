using System.Collections.Generic;

public interface INpsDialogueVisitor
{
    void Visit(DialogueNode node);
}

public interface IEventThemeVisitor : INpsDialogueVisitor
{
    bool ContainsTheme(EventTheme theme);
    IReadOnlyCollection<EventTheme> GetFoundThemes();
    void Clear();
}
