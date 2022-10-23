using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Dialogues : ScriptableObject
{
    //Tous les dialogues qui peuvent �tre lanc�s dans le jeu.
    public List<string> mom = new List<string>
    {
        "Maman : Bonjour mon ch�ri, il enfin temps pour toi d'aller chercher ton premier pokemon",
        "Maman : Le professeur Pin t'attend dans son laboratoire. Ne le fais pas attendre !"
    };
    public List<string> randomAnnoyingDude = new List<string>
    {
        "P�cheur : Fais attention ! Le monde est plein de pokemons sauvages!",
        "P�cheur : Si tu n'a pas de Pokemon pour te d�fendre et que tu te fait attaquer,",
        "P�cheur : cela pourrait mal tourner pour toi !"
    };
    public List<string> professorPine = new List<string>
    {
        "Professeur Pin : Bienvenue Red, il est grand temps que tu obtienne ton premier pokemon,",
        "Professeur Pin : Une grand aventure t'attend ! Choisis un des trois pokemons que voici."
    };
    public List<string> rivalBattle = new List<string>
    {
        "Red : He bien, il t'en aura fallu du temps, minable!",
        "Red : Maintenant que tu as enfin ton pokemon, voyons-voir lequel de nos deux pokemon est le plus fort !"
    };
    public List<string> rivalVictory = new List<string>
    {
        "Red : Nooon ! C'est impossible ! Comment as-tu pu gagner ?",
        "Red : Ce n'est que partie remise ! Je vais entra�ner mon pokemon d'arrache-pied et en capturer plein d'autre",
        "Red : On verra bien qui de nous deux est le plus fort, minable !"
    };
    public List<string> rivalDefeat = new List<string>
    {
        "Red : Pokemon maigrichon pour un dresseur de pacotille, vous allez bien ensemble.",
        "Red : Ne t'en veux pas trop, c'est probablement juste moi qui est trop fort.",
        "Red: Allez, � plus minable !"
    };
    public List<string> rivalMomBeforeLab = new List<string>
    {
        "M�re de Red : Salut Red ! Tu cherche Blue ? Il est d�j� parti au laboratoire du professeur !",
        "M�re de Red : Si tu te d�p�che, tu peux peut-�tre encore le rattraper."
    };
    public List<string> rivalMomAfterLab = new List<string>
    {
        "M�re de Red : Salut Red ! Tu cherche Blue ? Il a d�j� quitt� le village.",
        "M�re de Red : Il est toujours tr�s press� mais prend ton temps, ce n'est pas une course."
    };
    public List<string> end = new List<string>
    {
        "Une aventure fantastique vous attend dans le monde de pokemon",
        "To be continued"
    };
}