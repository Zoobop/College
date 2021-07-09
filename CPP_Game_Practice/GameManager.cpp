#include "GameManager.h"

GameManager::GameManager()
{
	playerParty = PlayerParty();
}

void GameManager::DisplayPlayer()
{
	auto activePlayer = playerParty.GetActivePlayer();

	if (activePlayer)
		activePlayer->ToString();
}
