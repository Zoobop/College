#pragma once
#include "Entity.h"
#include <iostream>

class DuelManager
{
public:
	DuelManager(Entity duelist1, Entity duelist2);

	int Attack(Entity attacker, Entity opponent);
	int CalculateDamage(Entity attacker, Entity opponent);
	bool IsDead(Entity combatent);
};

