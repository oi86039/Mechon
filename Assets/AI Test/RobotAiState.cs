namespace DisablerAi
{
    public enum RobotAiState
    {
        Start, // done
        Inactive, // done
        Alert, // done
        AlertCallHeadQuarters, // done
        AlertAttack, // done
        AlertReposition, // done
        AlertFollowUp, // done
        Patrol, // done
        PatrolMarchToEnd, // done
        PatrolMarchToStart, // done
        PatrolLookAround, // done
        Suspicion, // done
        SuspicionCallHeadQuarters, // done
        SuspicionFollowUp, // done
        SuspicionLookAround, // done
        SuspicionShrugOff, // done
        Searching,
        SearchingFollowUpPointOfInterest,
        SearchingLookAroundPointOfInterest,
        SearchingFollowUpPlayerLastSeen,
        SearchingLookAroundPlayerLastSeen,
        HeldUp, // done
        HeldUpDemandMarkAmmo, // done
        HeldUpDemandMarkEnemies, // done
        HeldUpRefuse, // done
        HeldUpGetDown, // done
        Hurt, // done
        Disabled // done
    }
}