#include "Attributes.h"

Attributes::Attributes()
{
}

Attributes::Attributes(unsigned int _max_health, unsigned int _current_health, unsigned int _max_stamina, unsigned int _current_stamina, unsigned int _strength, unsigned int _dexterity, unsigned int _defence)
{
	max_health = _max_health;
	current_health = _current_health;
	max_stamina = _max_stamina;
	current_stamina = _current_stamina;
	strength = _strength;
	dexterity = _dexterity;
	defence = _defence;
}

unsigned int Attributes::getMaxHealth()
{
	return max_health;
}

int Attributes::getCurrentHealth()
{
	return current_health;
}

unsigned int Attributes::getMaxStamina()
{
	return max_stamina;
}

int Attributes::getCurrentStamina()
{
	return current_stamina;
}

unsigned int Attributes::getStrength()
{
	return strength;
}

unsigned int Attributes::getDexterity()
{
	return dexterity;
}

unsigned int Attributes::getDefence()
{
	return defence;
}

void Attributes::changeHealth(int change)
{
	current_health += change;
}

void Attributes::changeStamina(int change)
{
	current_stamina += change;
}

void Attributes::setHealth(unsigned int value)
{
	current_health = value;
}

void Attributes::setStamina(unsigned int value)
{
	current_stamina = value;
}
