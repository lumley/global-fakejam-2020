using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Fakejam.Input;
using Fakejam.Units;
using Fakejam.Players;

public class CombatSceneManager : MonoBehaviour
{
    private List<SquadGroup> playerSquads;
    private List<SquadGroup> enemySquads;

    public SquadGroup squadPrefab;

    public BoxCollider2D playerSpawnArea;
    public BoxCollider2D enemySpawnArea;
    public GameObject squadsContainer;
    public GameObject squadMemberContainer;

    // Use this for initialization
    void Start()
    {
        Toolbox.Get<InputManager>().CombatSceneManager = this;
        CreateSquads();
    }

    void CreateSquads()
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

            int numFullSquads = Mathf.FloorToInt(squad.Count / squad.UnitDefinition.SquadSize);
            int numRemaining = squad.Count - (numFullSquads * squad.UnitDefinition.SquadSize);

            for (int i = 0; i < numFullSquads; i++)
            {
                spawnSquadGroup(owner, squad, squad.UnitDefinition.SquadSize);
            }
            if(numRemaining > 0)
            {
                spawnSquadGroup(owner, squad, squad.UnitDefinition.SquadSize);
            }
        }
    }
    
    
    private void spawnSquadGroup( PlayerType owner, Squad squadDef, int numMembers )
    {
        BoxCollider2D spawnArea = owner == PlayerType.Player ? playerSpawnArea : enemySpawnArea;
        List<SquadGroup> squadList = owner == PlayerType.Player ? playerSquads : enemySquads;

        SquadGroup group = Instantiate(squadPrefab, squadsContainer.transform);
        squadList.Add(group);

        group.transform.position = new Vector3(

            Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x),
            Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y),
            1f);

        group.spawnMembers(squadDef.Owner, squadDef.UnitDefinition, numMembers);

        group.name =
            (squadDef.Owner == PlayerType.Player ? "P_" : "E_") +
            (squadDef.UnitDefinition.PrefabOfUnit.name);
    }
}
