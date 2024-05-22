using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HeartFunFacts : MonoBehaviour
{
    public List<string> heartFacts;
    public List<string> kidneyFacts;
    public List<string> eyeFacts;
    public List<string> liverFacts;

    [SerializeField] private TextMeshPro signText;

    private void Start()
    {
        //fun facts heart
        heartFacts.Add("The average heart is the size of a fist in an adult");
        heartFacts.Add("Your heart will beat about 115,000 times each day");
        heartFacts.Add("Your heart pumps about 2,000 gallons of blood every day");
        heartFacts.Add("The heart can continue beating even when it’s disconnected from the body");
        heartFacts.Add("Most heart attacks happen on a Monday");
        heartFacts.Add("A woman’s heart beats slightly faster than a man’s heart");
        heartFacts.Add("It’s possible to have a broken heart, it’s called broken heart syndrome");
        heartFacts.Add("Your heart can sync to the rhythm when you listen to music");
        heartFacts.Add("The beating sound occurs because of the closing and opening of the heart’s valves");

        //fun facts kidney
        kidneyFacts.Add("Kidneys produce around 1 to 2 litres of urine every day");
        kidneyFacts.Add("Most people are born with two kidneys, but to live a full and healthy life, you only need one functioning kidney");
        kidneyFacts.Add("The amount of blood filtered by both kidneys is between 120 and 150 liters per day");
        kidneyFacts.Add("An adult’s kidney weighs about 142 grams and is about the size of a fist");
        kidneyFacts.Add("The largest kidney stone ever was the size of a coconut weighing about 1.1 kilograms");
        kidneyFacts.Add("The filtering structures are called nephrons. One kidney has more than one million of these");
        kidneyFacts.Add("Stretched end to end, the nephrons that make up the kidneys are about 8 kilometers long");

        //fun facts eye
        eyeFacts.Add("Your eyes can get sunburned");
        eyeFacts.Add("Ommatophobia is a fear of the eyes");
        eyeFacts.Add("All babies are color blind at birth");
        eyeFacts.Add("Blind people can see their dreams as long as they were not born blind");
        eyeFacts.Add("Some people have two different eye colors, a condition called heterochromia");
        eyeFacts.Add("Your eyes focus on 50 different objects every second");
        eyeFacts.Add("Your eyes can detect a candle flame from almost 3 kilometers away");
        eyeFacts.Add("Blue eyed people are more tolerant of alcohol, but less tolerant of the sun");
        eyeFacts.Add("We spend about 10% of our wake time with our eyes closed");
        eyeFacts.Add("The only organ more complex than the eye is the brain");
        eyeFacts.Add("80% of all learning comes through the eyes");
        eyeFacts.Add("Humans can see more shades of green than any other color");
        eyeFacts.Add("Your eyes can distinguish approximately 10 million different colors");
        eyeFacts.Add("Our ears and nose grow throughout life, but eyes stay the same size from birth");
        eyeFacts.Add("The older we get, the less tears we produce");
        eyeFacts.Add("You blink about 15-20 times in a minute");
        eyeFacts.Add("Only one sixth of your eyeball is visible");

        //fun facts liver
        liverFacts.Add("The liver can regenerate itself");
        liverFacts.Add("The liver is the largest solid internal organ in the body");
        liverFacts.Add("The liver can produce heat");
        liverFacts.Add("The liver can perform 500 different functions");
        liverFacts.Add("When you gain fat, the liver also does get more fat");
        liverFacts.Add("Without a liver a human does not survive alcohol consumption");
        liverFacts.Add("Without a liver, most medicine would be useless");
    }

    public void ChangeFunFactHeart()
    {
        for (;;){
            string currentText = signText.text;

            int randomIndex = Random.Range(0, heartFacts.Count);
            signText.text = heartFacts[randomIndex];

            if(signText.text != currentText){
                break;
            }
        }
    }

    public void ChangeFunFactKidney()
    {
        for (;;){
            string currentText = signText.text;

            int randomIndex = Random.Range(0, kidneyFacts.Count);
            signText.text = kidneyFacts[randomIndex];

            if(signText.text != currentText){
                break;
            }
        }
    }

    public void ChangeFunFactEye()
    {
        for (;;){
            string currentText = signText.text;

            int randomIndex = Random.Range(0, eyeFacts.Count);
            signText.text = eyeFacts[randomIndex];

            if(signText.text != currentText){
                break;
            }
        }
    }

    public void ChangeFunFactLiver()
    {
        for (;;){
            string currentText = signText.text;

            int randomIndex = Random.Range(0, liverFacts.Count);
            signText.text = liverFacts[randomIndex];

            if(signText.text != currentText){
                break;
            }
        }
    }
}
