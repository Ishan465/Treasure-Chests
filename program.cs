using System;

class Program
{
    static void Main(string[] args) //Main method
    {
        ManipulateTreasureChest();
        DesignTreasureChests();

        void ManipulateTreasureChest()
        {
            TreasureChest chest = new TreasureChest(); // creating object
            chest.ManipulateChest();
        }

        void DesignTreasureChests()
        {
            TreasureChest[] chests = new TreasureChest[2]; // creating array

            for (int index = 0; index < chests.Length; ++index)
            {
                Console.WriteLine($"Designing Treasure Chest #{index + 1}");
                chests[index] = new TreasureChest(); 
            }
            Console.WriteLine("\nThe following chests have been created");
            foreach (var chest in chests)
            {
                Console.WriteLine(chest); // printing tostring
            }
        }
    }
}

class TreasureChest
{
    private ChestState state = ChestState.Locked; //Setting initial state of chest to locked
    private readonly ChestMaterial _material;
    private readonly ChestLockType _lockType;
    private readonly ChestLootQuality _lootQuality;

    public TreasureChest() // constructor
    {
        _material = GetChestMaterial();
        _lockType = GetChestLockType();
        _lootQuality = GetChestLootQuality();
    }

    /// <summary>
    /// This method allows a user to go between the states of opening, locking and unlocking a treasure chest.
    /// If an invalid state transition is given, the state of the chest remains unchanged.
    /// </summary>
    public void ManipulateChest()
    {
        string tip = "You can write the following commands to manipulate the state of the chest: lock, unlock, open, close and end using end.\nRemember unlock and open are also different!";
        string tip2 = "Intially the chest is locked. The commands are not case sensitive";
        Console.WriteLine(tip);
        Console.WriteLine(tip2);

        while (true)
        {
            string prompt = $"The chest is {state}. What do yo want to do?: "; //Prompt to ask user
            Console.Write(prompt);  //Printing Prompt
            string userInput = Console.ReadLine();  //Asking user input
            string input = userInput.ToLower(); // converting to lower so it is not case sensitive

            if (input == "end" || input == "exit") //if exit then break
                break;
          
            if (input == "unlock") // If unlock
            {
                Unlock();
                continue;
            }
            else if (input == "open") // if open
            {
                Open();
                continue;
            }
            else if (input == "close") // if close
            {
                Close();
                continue;
            }
            else if (input == "lock") // if lock
            {
                Lock();
                continue;
            }
            else 
            { 
                Console.WriteLine(tip);
                continue;
            }
        }
    }
    // for methods, I used code from previous assignment but changed slightly as previously enums were in a different namespace
    private void Unlock() // unlock method
    {
        string isClosed = "The chest is already Closed";

        if (state == ChestState.Open)
        {
            Console.WriteLine("You can not unlock an opened chest!!!"); // Chest is opened so user can not unlock it
        }
        else if (state == ChestState.Closed)
        {
            Console.WriteLine(isClosed);        // Chest is closed so user can not unlock it
        }
        else if (state == ChestState.Locked)
        {
            state = ChestState.Closed;    // Chest is locked so user unlocks it
        }
    }

    private void Lock()
    {
        string closeToLock = $"The chest is {state}. You first need to close the chest to lock it.";
        string isLocked = "The chest is already Locked";

        if (state == ChestState.Open)
        {
            Console.WriteLine(closeToLock);     // Can not lock an opened chest
        }
        else if (state == ChestState.Closed)
        {
            state = ChestState.Locked;    //  lock the closed chest
        }
        else if (state == ChestState.Locked)
        {
            Console.WriteLine(isLocked);    // can not lock locked chest
        }
    }

    private void Open()
    {
        string unlockToOpen = $"The chest is {state}. You first need to unlock the chest to open it.";
        string isOpened = "The chest is already Opened";

        if (state == ChestState.Open)
        {
            Console.WriteLine(isOpened);    // opened chest can not be opened
        }
        else if (state == ChestState.Closed)
        {
            state = ChestState.Open;  // closed chest will be opened
        }
        else if (state == ChestState.Locked)
        {
            Console.WriteLine(unlockToOpen);    //lock chest can not be opened
        }
    }

    private void Close()
    {
        string isClosed = "The chest is already Closed";

        if (state == ChestState.Open)      // Open chest will be closed
        {
            state = ChestState.Closed;
        }
        else if (state == ChestState.Closed)
        {
            Console.WriteLine(isClosed);    // Chest is already closed so user can not close it
        }
        else if (state == ChestState.Locked)
        {
            Console.WriteLine("You can not close a locked chest!!!");   //Chest is locked so user can not close it
        }
    }
    public override string ToString() // To string method
    {
        string prompt = $"A {state} chest with the following properties:\nMaterial: {_material}\nLock Type: {_lockType}\nLoot Quality: {_lootQuality}";
        return prompt;
    }

    private ChestMaterial GetChestMaterial()
    {
        // some strings
        string prompt = ConsoleHelper("Material","Oak", "RichMahogany", "Iron", def:"Oak");
        string defaultMaterial = "Unable to parse command. Defaulting to Oak ";

        // Defaulting to Oak
        ChestMaterial material = ChestMaterial.Oak;
        Console.WriteLine(prompt);  // Printing prompt

        string userMaterialInput = Console.ReadLine();   // Asking user for material
        string userMaterial = userMaterialInput.ToLower(); // converting to lower case 

        switch (userMaterial)   // switch statement for getting user material
        {
            case "oak":
                return material;
            case "richmahogany":
                material = ChestMaterial.RichMahogany;
                return material;
            case "iron":
                material = ChestMaterial.Iron;
                return material;
            default:
                Console.WriteLine(defaultMaterial);
                return material;
        }
    }

    private ChestLockType GetChestLockType()
    {
        string prompt = ConsoleHelper("lock type", "Novice", "Intermediate", "Expert", def: "Novice");
        string defaultLock = "Unable to parse command. Defaulting to Novice ";

        // Defaulting to Novice
        ChestLockType lockType = ChestLockType.Novice;
        Console.WriteLine(prompt);

        string userLockInput = Console.ReadLine();   // Asking user for lock tupe
        string userLock = userLockInput.ToLower();

        switch (userLock)   // switch statement for getting user lock type
        {
            case "novice":
                return lockType;
            case "intermediate":
                lockType = ChestLockType.Intermediate;
                return lockType;
            case "expert":
                lockType = ChestLockType.Expert;
                return lockType;
            default:
                Console.WriteLine(defaultLock);
                return lockType;
        }
    }

    ChestLootQuality GetChestLootQuality()
    {
        string prompt = ConsoleHelper("loot qualtiy", "Grey", "Green", "Purple", def: "Grey");
        string defaultQualtiy = "Unable to parse command. Defaulting to Grey ";

        // Defaulting to Grey
        ChestLootQuality lootQualtiy = ChestLootQuality.Grey;
        Console.WriteLine(prompt);

        string userLootInput = Console.ReadLine();   // Asking user for loot tupe
        string userLoot = userLootInput.ToLower();

        switch (userLoot)   // switch statement for getting user loot type
        {
            case "grey":
                return lootQualtiy;
            case "green":
                lootQualtiy = ChestLootQuality.Green;
                return lootQualtiy;
            case "purple":
                lootQualtiy = ChestLootQuality.Purple;
                return lootQualtiy;
            default:
                Console.WriteLine(defaultQualtiy);
                return lootQualtiy;
        }
    }
    // changed to value returing function so can save it in variable
    // also added 2 new agruments prop and def to make statement more precise
    private string ConsoleHelper(string prop,string prop1, string prop2, string prop3, string def) 
    {
        string prompt = $"Choose a {prop} with which to construct a chest.\nChoose from the following properties.\n1.{prop1}\n2.{prop2}\n3.{prop3} (default {def})";
        return prompt;
    }
    // enums used for code here it is slightly different than previous code as previously it was in seperate namespace
    public enum ChestState { Open, Closed, Locked };
    public enum ChestMaterial { Oak, RichMahogany, Iron };
    public enum ChestLockType { Novice, Intermediate, Expert };
    public enum ChestLootQuality { Grey, Green, Purple };
}
