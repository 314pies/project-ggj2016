using UnityEngine;
using System.Collections.Generic;

public class PlayerControl
{
	
	public NetworkControllerInGame NetworkController;
	
	private List<Character> AICharacter = null;
	private Character character = null;
	
	public PlayerControl( Character character, List<Character> AICharacter )
	{
		this.character = character;
		this.AICharacter = AICharacter;
	}

    public void Tick()
    {
        if (!GameManager.DisableLocalControl)
        {
            character.Tick();

            if (!NetworkControllerInGame.LockLocalMovement)
                character.Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            character.DoAction(Input.GetKey(KeyCode.Space));
            if (Input.GetKeyDown(KeyCode.Z))
            {
                if (character.IsAvailableToPushOthers())
                {
                    character.Push();
                    NetworkController.SynPushAnim();
                    for (int i = 0; i < AICharacter.Count; ++i)
                    {
                        if (character.IsInPushRange(AICharacter[i].GetPosition()))
                        {
                            Vector2 forceDir = (AICharacter[i].GetPosition() - character.GetPosition()).normalized;
                            //AICharacter[ i ].Fall( forceDir );
                            int Index = AICharacter[i].NetWorkingId;
                            if (AICharacter[i].IsRemotePlayer == false)
                            {
                                NetworkController.PushAI(Index, forceDir.x, forceDir.y);
                            }
                            else
                            {
                                Debug.Log("Hey is a remote player: " + Index);
                                NetworkController.PushPlayer(Index, forceDir.x, forceDir.y);
                            }
                        }
                    }
                }
            }
        }
    }
}