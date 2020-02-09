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

        spawnSquadGroupsFromList(playerSquadDefs, playerSquads, playerSpawnArea);
        spawnSquadGroupsFromList(enemySquadDefs, enemySquads, enemySpawnArea);
    }

    private void spawnSquadGroupsFromList( List<Squad> squadDefist, List<SquadGroup> squadGroupList, BoxCollider2D spawnArea)
    {
        foreach (Squad squad in squadDefist)
        {

            int numFullSquads = Mathf.FloorToInt(squad.Count / squad.UnitDefinition.SquadSize);
            int numRemaining = squad.Count - (numFullSquads * squad.UnitDefinition.SquadSize);

            for (int i = 0; i < numFullSquads; i++)
            {
                spawnSquadGroup(squad, squad.UnitDefinition.SquadSize, spawnArea, squadGroupList);
            }
            if(numRemaining > 0)
            {
                spawnSquadGroup(squad, squad.UnitDefinition.SquadSize, spawnArea, squadGroupList);
            }
        }
    }
    
    
    private void spawnSquadGroup( Squad squadDef, int numMembers, BoxCollider2D spawnArea, List<SquadGroup> squadList )
    {
        SquadGroup group = Instantiate(squadPrefab, squadsContainer.transform);
        squadList.Add(group);
        group.owner = squadDef.Owner;

        group.name =
            (squadDef.Owner == PlayerType.Player ? "P_" : "E_") +
            (squadDef.UnitDefinition.PrefabOfUnit.name);

        group.transform.position = new Vector3(

            Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x),
            Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y),
            1f);

        group.spawnMembers(squadDef.UnitDefinition, numMembers);
    }
}
