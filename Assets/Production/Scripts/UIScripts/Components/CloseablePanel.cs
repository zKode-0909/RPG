using UnityEngine;
using UnityEngine.UIElements;

public class CloseablePanel : VisualElement
{

    Button closeButton;
    VisualElement contentHolder;
    bool hasContent;


    public CloseablePanel(int height,int width) {
        AddToClassList("closeablePanel");

        contentHolder = new VisualElement();

        contentHolder.AddToClassList("closeablePanelContentHolder");



        closeButton = new Button();

        closeButton.clicked += ClosePanel;

        closeButton.AddToClassList("closeButton");

        this.style.height = height;
        this.style.width = width;


        Add(closeButton);
        Add(contentHolder);
        

    }

    void ClosePanel() {
        this.style.display = DisplayStyle.None;
    }

    public bool TryAddContent(VisualElement content) {
        if (hasContent)
        {
            return false;
        }
        else { 
            contentHolder.Add(content);
            hasContent = true;
            return true;
        }
    }

    public void ClearContent() {
        if (hasContent) { 
            hasContent = false;
            contentHolder.Clear();
        }
    }

    public bool HasContent() { 
        return hasContent;
    }


}
