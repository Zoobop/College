#include "CharacterCreator.h"

CharacterCreator::CharacterCreator()
{
	std::cout << "\n\n\n" << std::endl;
}

PlayerCharacter CharacterCreator::TransferPlayer()
{
	return created_player;
}

void CharacterCreator::setCreatedPlayer(PlayerCharacter _player)
{
	created_player = _player;
}
