using UnityEngine;
using System.Collections.Generic;

public class PlayerControl
{
    private List<Character> AICharacter = null;
    private Character character = null;

    public PlayerControl( Character character, List<Character> AICharacter )
    {
        this.character = character;
        this.AICharacter = AICharacter;
    }

    public void Tick()
    {
        character.Tick();

        character.Move( Input.GetAxis( "Horizontal" ), Input.GetAxis( "Vertical" ) );
        character.DoAction( Input.GetKey( KeyCode.Space ) );
        if ( Input.GetKeyDown( KeyCode.Z ) )
        {
            if ( character.IsAvailableToPushOthers() )
            {
                character.Push();
                for ( int i = 0; i < AICharacter.Count; ++i )
                {
                    if ( character.IsInPushRange( AICharacter[ i ].GetPosition() ) )
                    {
                        Vector2 forceDir = ( AICharacter[ i ].GetPosition() - character.GetPosition() ).normalized;
                        AICharacter[ i ].Fall( forceDir );
                    }
                }
            }
        }
    }
}