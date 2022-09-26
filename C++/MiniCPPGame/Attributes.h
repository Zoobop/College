#pragma once
class Attributes
{
public:
	Attributes();
	Attributes(unsigned int _max_health, unsigned int _current_health, unsigned int _max_stamina, unsigned int _current_stamina, unsigned int _strength, unsigned int _dexterity, unsigned int _defence);

	// Getters & Setters
	unsigned int getMaxHealth();
	int getCurrentHealth();
	unsigned int getMaxStamina();
	int getCurrentStamina();
	unsigned int getStrength();
	unsigned int getDexterity();
	unsigned int getDefence();

	void changeHealth(int change);
	void changeStamina(int change);
	void setHealth(unsigned int value);
	void setStamina(unsigned int value);

private:
	unsigned int max_health;
	int current_health;
	unsigned int max_stamina;
	int current_stamina;
	unsigned int strength;
	unsigned int dexterity;
	unsigned int defence;
};

