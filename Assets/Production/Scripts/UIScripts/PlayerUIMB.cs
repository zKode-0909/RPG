using System.ComponentModel.Design;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerUIMB : MonoBehaviour
{
    [SerializeField] StyleSheet style;
    [SerializeField] UIDocument document;

    VisualElement root;

    QuestLogView questLogView;
    VisualElement panelHolder;

    EventBinding<QuestLogEvent> QuestLogEventBinding;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (root == null) { 
            root = document.rootVisualElement;
            root.AddToClassList("uiRoot");
            root.styleSheets.Add(style);

            panelHolder = new VisualElement();

            QuestLogEventBinding = new EventBinding<QuestLogEvent>(OpenQuestLog);
            EventBus<QuestLogEvent>.Register(QuestLogEventBinding);

            questLogView = new QuestLogView();
        }
    }


    void OpenQuestLog() {
        if (questLogView != null) {
            TryAddToPanelHolder(questLogView);
        }
        
    }

    bool TryAddToPanelHolder(VisualElement panelContent) {

        var added = false;

        foreach (var child in panelHolder.Children()) {
            if (child is CloseablePanel) { 
                var panel = (CloseablePanel) child;
                if (!panel.HasContent()) {
                    if (panel.TryAddContent(panelContent)) {
                        added = true;
                    }
                    
                }

            }
        }

        if (!added) {
            var panel = panelHolder.Children().FirstOrDefault();
            if (panel != null && panel is CloseablePanel) {
                var closeablePanel = (CloseablePanel)panel;
                closeablePanel.ClearContent();
                if (closeablePanel.TryAddContent(panelContent)) { 
                    return true;
                }
            }
            
        }

        return false;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
