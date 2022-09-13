#include "GameManager.h"

GameManager::GameManager()
{
    std::cout << "Arena" << "\n------------------------------" << std::endl;
    std::cout << "0) Play\n" << "1) Quit\n" << std::endl;

    CharacterCreator creator = CharacterCreator();

    bool run = true;
    while (run) {
        int input;
        std::cout << "Choice: ";
        std::cin >> input;

        switch (input) {
        case 0:
            player = creator.TransferPlayer();
            break;
        case 1:
            run = false;
            break;
        }
    }

    PlayerCharacter player = PlayerCharacter("Player", Attributes(100, 100, 100, 100, 10, 10, 10), weapons_armor.Sword_AlphaChrozine(), 0);
    Enemy enemy = Enemy("Enemy", Attributes(70, 70, 100, 100, 10, 10, 10), weapons_armor.Sword_Iron(), 1, 10, 0);

    DuelManager duel = DuelManager(player, enemy);
}

PlayerCharacter GameManager::getPlayer()
{
    return player;
}

void GameManager::setPlayer(PlayerCharacter _player)
{
    player = _player;
}
