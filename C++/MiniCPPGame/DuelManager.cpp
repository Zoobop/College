#include "DuelManager.h"

DuelManager::DuelManager(Entity duelist1, Entity duelist2)
{
	Entity player = duelist1;
	Entity opponent = duelist2;

	std::cout << player.getName() << " VS " << opponent.getName() << "\n" << std::endl;

	Entity combatents[2]{ player, opponent };;

	bool run;
	if (player.getAttributes().getDexterity() >= opponent.getAttributes().getDexterity()) {
		run = true;
	}
	else {
		std::swap(combatents[0], combatents[1]);
		run = true;
	}

	while (run) {
		int index = 1;
		combatents[index].setAttribute(0, Attack(combatents[index-1], combatents[index]));

		if (IsDead(combatents[index])) {
			std::cout << combatents[index].getAttributes().getCurrentHealth() << "\n" << std::endl;
			std::cout << combatents[index-1].getName() << " has won the duel!" << std::endl;
			break;
		} else {
			std::cout << combatents[index].getAttributes().getCurrentHealth() << "\n" << std::endl;
			std::swap(combatents[0], combatents[1]);
		}
	}
}

int DuelManager::Attack(Entity attacker, Entity opponent)
{
	unsigned int random = rand() % 10 + 1;
	int overall_damage = 0;

	if (attacker.getAttributes().getDexterity() >= random) {
		overall_damage -= CalculateDamage(attacker, opponent);
		std::cout << attacker.getName() << " hit " << opponent.getName() << " for " << overall_damage * -1 << " damage!" << std::endl;
		return overall_damage;
	} else {
		std::cout << attacker.getName() << "'s attack missed!" << "\n" << std::endl;
		return 0;
	}
}

int DuelManager::CalculateDamage(Entity attacker, Entity opponent)
{
	int full_damage;
	int overflow = 0;
	int strength = (attacker.getAttributes().getStrength() / 2);
	int weapon_damage = attacker.getWeapon().getDamage();
	int takeaway = opponent.getAttributes().getDefence() / 5;

	unsigned int random = rand() % 10 + 1;
	if (attacker.getWeapon().getCrit() >= random) {
		weapon_damage *= 2;
		std::cout << "Critical Hit! ";
	}
	full_damage = weapon_damage - takeaway;

	if (opponent.getAttributes().getCurrentHealth() - full_damage <= 0) {
		overflow = opponent.getAttributes().getCurrentHealth() - full_damage;
	}
	return weapon_damage - takeaway + (overflow);
}

bool DuelManager::IsDead(Entity combatent)
{
	if (combatent.getAttributes().getCurrentHealth() <= 0) {
		return true;
	} else {
		return false;
	}
}
