using System.Collections.Generic;
using Fakejam.Input;
using Fakejam.Players;
using Fakejam.Units;
using UnityEngine;
using UnityEngine.Events;

public class CombatSceneManager : MonoBehaviour
{
    private List<SquadGroup> playerSquads;
    private List<SquadGroup> enemySquads;

    public SquadGroup squadPrefab;

    public BoxCollider2D playerSpawnArea;
    public BoxCollider2D enemySpawnArea;
    public GameObject squadsContainer;
    public GameObject squadMemberContainer;

    public UnityEvent OnPlayerWon;
    public UnityEvent OnPlayerLost;

    // Use this for initialization
    private void Start()
    {
        Toolbox.Get<InputManager>().CombatSceneManager = this;
        CreateSquads();
    }

    private void CreateSquads()
    {
        BattleManager battleManager = Toolbox.Get<BattleManager>();
        List<Squad> playerSquadDefs = battleManager.GetSquadsForOwner(PlayerType.Player);
        List<Squad> enemySquadDefs = battleManager.GetSquadsForOwner(PlayerType.Enemy1);

        playerSquads = new List<SquadGroup>();
        enemySquads = new List<SquadGroup>();

        spawnSquadGroupsFromList(PlayerType.Player, playerSquadDefs);
        spawnSquadGroupsFromList(PlayerType.Enemy1, enemySquadDefs);
    }

    private void spawnSquadGroupsFromList( PlayerType owner, List<Squad> squadDefist)
    {
        foreach (Squad squad in squadDefist)
        {
            int numFullSquads = squad.Count / squad.UnitDefinition.SquadSize;
            int numRemaining = squad.Count - numFullSquads * squad.UnitDefinition.SquadSize;

            for (int i = 0; i < numFullSquads; i++)
            {
                spawnSquadGroup(owner, squad, squad.UnitDefinition.SquadSize);
            }
            if(numRemaining > 0)
            {
                spawnSquadGroup(owner, squad, numRemaining);
            }
        }
    }
    
    
    private void spawnSquadGroup( PlayerType owner, Squad squadDef, int numMembers )
    {
        BoxCollider2D spawnArea = owner == PlayerType.Player ? playerSpawnArea : enemySpawnArea;
        List<SquadGroup> squadList = owner == PlayerType.Player ? playerSquads : enemySquads;

        SquadGroup group = Instantiate(squadPrefab, squadsContainer.transform);
        group.OnSquadDied.AddListener(OnSquadDied);
        squadList.Add(group);

        var spawnAreaBounds = spawnArea.bounds;
        group.transform.position = new Vector3(

            Random.Range(spawnAreaBounds.min.x, spawnAreaBounds.max.x),
            Random.Range(spawnAreaBounds.min.y, spawnAreaBounds.max.y),
            1f);

        group.spawnMembers(squadDef.Owner, squadDef.UnitDefinition, numMembers);

        group.name =
            (squadDef.Owner == PlayerType.Player ? "P_" : "E_") +
            (squadDef.UnitDefinition.PrefabOfUnit.name);
    }

    private void OnSquadDied(SquadGroup deadSquad)
    {
        if (playerSquads.Remove(deadSquad) && playerSquads.Count == 0)
        {
            OnLose();
            return;
        }

        if (enemySquads.Remove(deadSquad) && enemySquads.Count == 0)
        {
            OnWin();
        }
    }

    private void OnWin()
    {
        WriteAllUnits();
        OnPlayerWon?.Invoke();
    }

    private void OnLose()
    {
        WriteAllUnits();
        var progressionManager = Toolbox.Get<ProgressionManager>();
        progressionManager.Restart();
        OnPlayerLost?.Invoke();
    }

    private void WriteAllUnits()
    {
        
        List<Squad> squads = new List<Squad>();
        foreach (var playerSquad in playerSquads)
        {
            var playerSquadUnitType = playerSquad.UnitType;
            var unitCount = playerSquad.GetUnitCount();
            
            squads.Add(new Squad
            {
                Count = unitCount,
                UnitDefinition = playerSquadUnitType,
                Owner = playerSquad.owner
            });
        }
        
        var battleManager = Toolbox.Get<BattleManager>();
        battleManager.SetSquadsForCombat(squads);
    }
}
