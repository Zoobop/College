#include "PlayerParty.h"

PlayerParty::PlayerParty()
{
	partyMembers = std::array<Player*, PARTY_CAP>();
	currentSize = 0;
	activeIndex = 0;
}

bool PlayerParty::JoinParty(Player* player)
{
	// Check if at party size cap
	if (currentSize == PARTY_CAP) {
		std::cout << "Party is at max capacity!" << std::endl;
		return false;
	}

	// Check if player is valid
	if (!player) {
		std::cout << "Invalid Player!" << std::endl;
		return false;
	}

	// Check if player is already in party
	for (size_t i = 0; i < currentSize; i++)
		if (partyMembers[i] == player) {
			std::cout << "Player is already in Party!" << std::endl;
			return false;
		}

	// Add player to party
	for (size_t i = 0; i < PARTY_CAP; i++)
		if (partyMembers[i] == nullptr) {
			partyMembers[i] = player;
			std::cout << "Player successfully added!" << std::endl;

			currentSize++;
			return true;
		}

	return false;
}

bool PlayerParty::LeaveParty(Player* member)
{
	// Check if member is the only member
	if (currentSize == 1) {
		std::cout << "Party must have at least one player!" << std::endl;
		return false;
	}

	// Check if member is valid
	if (!member) {
		std::cout << "Invalid Player!" << std::endl;
		return false;
	}

	// Check if player exists within party -- removes member if so
	for (size_t i = 0; i < currentSize; i++) {
		if (partyMembers[i] != member) {
			std::cout << "Player does not exist within Party!" << std::endl;
			return false;
		}
		else if (partyMembers[i] == member) {
			partyMembers[i] = nullptr;
			std::cout << "Member successfully removed!" << std::endl;

			currentSize--;
			return true;
		}
	}

	return false;
}

void PlayerParty::NextMember()
{
	if (activeIndex == PARTY_CAP - 1)
		activeIndex = 0;
	else
		activeIndex++;
}

void PlayerParty::PreviousMember()
{
	if (activeIndex == 0)
		activeIndex = PARTY_CAP - 1;
	else
		activeIndex--;
}
