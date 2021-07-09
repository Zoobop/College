// CPP_Game_Practice.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include "GameManager.h"

int main()
{
    GameManager gm = GameManager();

    // PlayerParty Testing

    Player* player1 = new Player(); player1->number = 1; player1->SetName("Player 1");
    Player* player2 = new Player(); player2->number = 2; player2->SetName("Player 2");
    Player* player3 = new Player(); player3->number = 3; player3->SetName("Player 3");

    std::array<Player*, 3> players = { player1, player2, player3 };

    for (auto& player : players)
        gm.playerParty.JoinParty(player);

    gm.DisplayPlayer();

    gm.playerParty.NextMember();

    gm.DisplayPlayer();

    gm.playerParty.NextMember();

	gm.DisplayPlayer();

	gm.playerParty.NextMember();

	gm.DisplayPlayer();

	gm.playerParty.PreviousMember();

	gm.DisplayPlayer();

	gm.playerParty.PreviousMember();

	gm.DisplayPlayer();

	gm.playerParty.PreviousMember();

    gm.DisplayPlayer();

    std::cout << std::endl;

    // Equipment testing

    Weapon* sword = new Weapon(); sword->Sword();
    Weapon* hammer = new Weapon(); hammer->Hammer();

    gm.playerParty.GetActivePlayer()->GetEquipment().AddWeapon(sword);
    gm.playerParty.GetActivePlayer()->GetEquipment().DisplayWeapon();

	gm.playerParty.GetActivePlayer()->GetEquipment().AddWeapon(hammer);
	gm.playerParty.GetActivePlayer()->GetEquipment().DisplayWeapon();

    gm.playerParty.NextMember();
    gm.playerParty.GetActivePlayer()->GetEquipment().DisplayWeapon();

    gm.playerParty.PreviousMember();
    gm.playerParty.GetActivePlayer()->GetEquipment().DisplayWeapon();
    gm.playerParty.GetActivePlayer()->GetEquipment().NextWeapon();
}

// Run program: Ctrl + F5 or Debug > Start Without Debugging menu
// Debug program: F5 or Debug > Start Debugging menu

// Tips for Getting Started: 
//   1. Use the Solution Explorer window to add/manage files
//   2. Use the Team Explorer window to connect to source control
//   3. Use the Output window to see build output and other messages
//   4. Use the Error List window to view errors
//   5. Go to Project > Add New Item to create new code files, or Project > Add Existing Item to add existing code files to the project
//   6. In the future, to open this project again, go to File > Open > Project and select the .sln file
