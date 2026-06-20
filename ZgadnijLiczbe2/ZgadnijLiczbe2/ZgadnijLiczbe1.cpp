#include <iostream>
#include <fstream>
#include <cmath>
#include <ctime>
#include <cstdlib>
#include <vector>
#include <algorithm>
#include <string>

using namespace std;

vector<string> levels_in_order = {"Easy  ", "Medium", "Hard  "};

vector<vector<string>> easyDB={
    /**{"2", "EasyTest1"},
    {"3", "EasyTest2"},
    {"4", "EasyTest3"},
    {"5", "EasyTest4"},
    {"6", "EasyTest5"}**/
    }; 

    vector<vector<string>> mediumDB={
    /**{"2", "MediumTest1"},
    {"3", "MediumTest2"},
    {"4", "MediumTest3"},
    {"5", "MediumTest4"},
    {"6", "MediumTest5"}**/
    }; 

    vector<vector<string>> hardDB={
    /**{"2", "HardTest1"},
    {"3", "HardTest2"},
    {"4", "HardTest3"},
    {"5", "HardTest4"},
    {"6", "HardTest5"}**/
    }; 

string too_high_rand_comm(){
    vector<string> comm = {
        "Za duzo!",
        "Mysle o mniejszej liczbie!",
        "Przesadziles!",
        "Sprobuj mniej!"
    };
    return comm[rand()%comm.size()];
}

string too_low_rand_comm(){
    vector<string> comm = {
        "Za malo!",
        "Mysle o wiekszej liczbie!",
        "Sprobuj wiecej!",
        "To za nisko!"
    };
    return comm[rand()%comm.size()];
}

void sort_db(vector<vector<string>>& db){
    sort(db.begin(), db.end(), [](auto &a, auto &b){
        return stoi(a[0]) < stoi(b[0]);
    });
}

void save_to_scoreboard(string difficulty, int tries, string nick){
    vector<vector<string>>* db = nullptr;

    if(difficulty=="1") db = &easyDB;
    else if(difficulty=="2") db = &mediumDB;
    else if(difficulty=="3") db = &hardDB;

    if(!db) return;

    db->push_back({to_string(tries), nick});
    sort_db(*db);

    if(db->size() > 5)
        db->pop_back();
}

string generate_number(string difficulty){
    int magic_number;
    if(difficulty=="1"){
        magic_number=1+rand()%51;
    }
    else if(difficulty=="2"){
        magic_number=1+rand()%101;
    }
    else{
        magic_number=1+rand()%251;
    }
    return to_string(magic_number);
}

int game(string difficulty){
    string given_number;
    string magic_number = generate_number(difficulty);
    int tries=0;

    string zaklad;
    int max_tries = -1;

    system("cls");
    cout<<"Czy chcesz tryb zakladu? (t/n): ";
    cin>>zaklad;

    if(zaklad=="t"){
        cout<<"Podaj maksymalna liczbe prob: ";
        cin>>max_tries;
        max_tries=max_tries+1;
    }

    do{
        system("cls");
        tries++;
        if(tries >= max_tries && max_tries!=-1){
            cout<<"\nPrzegrales zaklad!\n";
            system("pause");
            return -1;
        }
        if(tries>=2){
            try{
                if(stoi(given_number)>stoi(magic_number)){
                    cout<<too_high_rand_comm()<<"\n";
                }
                else{
                    cout<<too_low_rand_comm()<<"\n";
                }
            }
            catch (...){
                cout<<"Podaj poprawna liczbe!\n";
            }
        }

        cout << "\n==========================================\n";
        cout << "|                                       |\n";
        cout << "|     A wiec o jakiej liczbie mysle     |\n";
        cout << "|                                       |\n";
        cout << "==========================================\n\n";
        cout << "Numer twojej proby: "<<tries<<"\n\n";
        cout << "Sekret: " <<magic_number;
        cin >> given_number;


    }
    while (given_number!=magic_number);

    return tries;
}

void pokazTOP5(int diff){
    system("cls");
    cout << "\n==========================================\n";
    cout << "|                                       |\n";
    cout << "|        TOP 5 na poziomie "<<levels_in_order[diff-1]<<"       |\n";
    cout << "|                                       |\n";
    cout << "==========================================\n\n";

    vector<vector<string>> temp_vect = {};
    switch(diff){
        case 1:
            temp_vect=easyDB;
        break;
        case 2:
            temp_vect=mediumDB;
        break;
        case 3:
            temp_vect=hardDB;
        break;
        default:
        break;
    }
    string delay;
    if(!temp_vect.empty()){
        cout << "Miejsce    Proby   Nick\n\n";
        for (int i = 0; i < temp_vect.size(); i++) {
            cout <<"Miejsce "<<i+1<<":  ";
            for (int j = 0; j < temp_vect[i].size(); j++) {
                cout <<temp_vect[i][j] << "    ";
            }
        cout << endl;
        }
    }
    else{
        cout<<"\nNie ma jeszcze zadnych zapisanych wynikow\n";
    }
    cout<<"\n\nWpisz jakikolwek symbol aby wyjsc\n";
    cin>>delay;
}

void scoreboard_menu(){
    while(true){
        system("cls");
        string scoreboard_menu_input;
        cout << "\n==========================================\n";
        cout << "|                                       |\n";
        cout << "|        Wybierz poziom trudnosci       |\n";
        cout << "|                                       |\n";
        cout << "==========================================\n\n";
        cout << "1  Easy\n";
        cout << "2  Medium\n";
        cout << "3  Hard\n";
        cout << "4  Cofnij\n";
        cin >> scoreboard_menu_input;

        if(scoreboard_menu_input=="4"){
            return;
        }
        if(scoreboard_menu_input=="1" || scoreboard_menu_input=="2" || scoreboard_menu_input=="3"){

            pokazTOP5(stoi(scoreboard_menu_input));
        }
    }
}

void menu_trudnosc(){
    while(true){
        system("cls");

        string menu_input_id;
        cout << "\n==========================================\n";
        cout << "|                                       |\n";
        cout << "|        Wybierz poziom trudnosci       |\n";
        cout << "|                                       |\n";
        cout << "==========================================\n\n";
        cout << "1  Easy: Liczby od 1 do 50\n";
        cout << "2  Medium: Liczby od 1 do 100\n";
        cout << "3  Hard: Liczby od 1 do 250\n";
        cout << "4  Cofnij\n";
        cin >> menu_input_id;

        if(menu_input_id=="1" || menu_input_id=="2" || menu_input_id=="3"){
            int tr = game(menu_input_id);
            if(tr < 0) return;

            system("cls");
            cout<<"Udalo Ci sie wygrac w "<<tr<<" probie na poziomie trudnosci "<<levels_in_order[stoi(menu_input_id)-1]<<".\n";

            cout<<"Podaj imie: ";
            string nick;
            cin>>nick;
            save_to_scoreboard(menu_input_id, tr, nick);
        }
        if(menu_input_id=="4")
            break;
    }
}

string menu_powitalne(){
    while(true){
        system("cls");
        string welcome_input_id;
        cout << "\n==========================================\n";
        cout << "|                                       |\n";
        cout << "|            Zgadnij Liczbe             |\n";
        cout << "|                                       |\n";
        cout << "==========================================\n\n";
        cout << "1  Rozpocznij nowa gre\n";
        if (!easyDB.empty() || !mediumDB.empty() || !hardDB.empty()){
            cout << "2  Scoreboard\n";
        }
        cout << "0 Exit\n";
        cin >> welcome_input_id;

        if (welcome_input_id=="1"){
            menu_trudnosc();
        }
        else if (welcome_input_id=="2" && (!easyDB.empty() || !mediumDB.empty() || !hardDB.empty())){
            scoreboard_menu();
        }
        else if (welcome_input_id=="0"){
            return "0";
        }
    }
}

int main()
{
    srand(time(0));
    menu_powitalne();
}
