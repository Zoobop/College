#pragma once

#include "Player.h"

class PlayerParty
{
public:
	PlayerParty();

	bool JoinParty(Player* player);
	bool LeaveParty(Player* member);

	void NextMember();
	void PreviousMember();

	inline Player* GetActivePlayer() { return partyMembers[activeIndex]; }

	const inline size_t& GetCurrentSize() { return currentSize; }

	static const size_t PARTY_CAP = 3;

private:
	std::array<Player*, PARTY_CAP> partyMembers;
	size_t currentSize;
	size_t activeIndex;

};

