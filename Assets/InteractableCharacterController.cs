using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableCharacterController : InvestigationObject
{
    [System.Serializable]
    public class Topic
    {
        public string TopicName;
        public int TopicFRM;
        public bool talked;
        public bool locked;

    }
   
    [System.Serializable]
    public class PresentableEvidence
    {
        public string EvidenceName;
        public int EvidenceFRM;
        public bool talked;
        public bool required;

    }

    public string characterName;
    public List<Topic> Topics;
    public List<PresentableEvidence> ToPresent;
    public int AnyEvidenceFRM;
    public int backToInvestigationFRM;
    public PresentableEvidence EvidenceFromName(string name)
    {
        foreach (PresentableEvidence pe in ToPresent)
        {
            if (pe.EvidenceName == name)
            {
                return pe;
            }
        }
        return null;
    }

    public void Update()
    {
        Investigated = actuallyInvestigated();
    }

    public bool actuallyInvestigated()
    {
        if (talkedAboutEverything() && presentedEveryRequiredEvidence())
        {
            return true;
        }
        return false;
    }


    public bool talkedAboutEverything()
    {
        foreach (Topic topic in Topics)
        {
            if (!topic.talked || topic.locked)
            {
                return false;
            }

        }
        return true;

    }

    public bool presentedEveryRequiredEvidence()
    {
        foreach (PresentableEvidence evidence in ToPresent)
        {
            if (evidence.required && !evidence.talked)
            {
                return false;
            }
        }
        return true;
    }


}
