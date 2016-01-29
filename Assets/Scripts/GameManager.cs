using UnityEngine;
using System.Collections.Generic;

public class GameManager
{
    private Character player = null;
    private List<Character> aiCharacterList = null;
    private Master master = null;

    public GameManager( Character player, List<Character> aiCharacterList, Master master )
    {
        this.player = player;
        this.aiCharacterList = aiCharacterList;
        this.master = master;
    }

    public void Tick()
    {
        for ( int i = 0; i < aiCharacterList.Count; ++i )
        {
            if ( master.IsInCircleRange( aiCharacterList[ i ].GetPosition() ) == true )
            {
                if ( aiCharacterList[ i ].IsDoingAction() == false )
                    aiCharacterList[ i ].Kill();
            }
        }

        if ( master.IsInCircleRange( player.GetPosition() ) == true )
        {
            if ( player.IsDoingAction() == false )
            {
                player.Kill();
            }
        }

        if ( Input.GetKeyDown( KeyCode.Alpha1 ) )
        {
            ResetGame();
        }
    }

    private void ResetGame()
    {
        for ( int i = 0; i < aiCharacterList.Count; ++i )
        {
            aiCharacterList[ i ].Reset();
        }
        player.Reset();
    }
}