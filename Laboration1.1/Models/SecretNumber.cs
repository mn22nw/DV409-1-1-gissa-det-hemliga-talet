using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Collections.ObjectModel;

namespace Laboration1._1.Models
{
    public class SecretNumber
    {   
        #region Fields
        private int? _number; 
        public const int maxNumber = 100;
        public const int minNumber = 1;
        private List<GuessedNumber> _guessedNumbers; 

        private GuessedNumber _lastGuessedNumber; 

        public static int MaxNumberOfGuesses = 7;  
        #endregion

        public struct GuessedNumber
        {
            public int? Number;  
            public Outcome Outcome; 
        };

        public enum Outcome
        {
            Low,
            High,
            Right,
            NoMoreGuesses,
            OldGuess,
        }

        #region Properties

        [DisplayName("Gissa ett tal mellan 1 och 100:")]    
        [Required(ErrorMessage = "Nummer måste anges.")]
        [StringLength(3)]
        [Range(1, 100, ErrorMessage = "Nummer måste anges.")]
        public int? Number 
        { 
            get
            {
                return CanMakeGuess ? default(int?) : _number; // är null så länge en gissning kan göras
            }

            private set
            {
                _number = value;
                 
            }
        }

        public bool CanMakeGuess
        {
            get
            {  
                if (Count < MaxNumberOfGuesses)
                {
                    return true;
                }

                return false;
            }
        }

        public int Count
        {  
            get
            {
                return GuessedNumbers.Count; 
            }
        }

        public IList<GuessedNumber> GuessedNumbers
        { 
            get
            {
               IList<GuessedNumber> GuessedNumbers = _guessedNumbers.AsReadOnly();
               return GuessedNumbers;
            }
        }

        public GuessedNumber LastGuessedNumber
        {   
            get
            {
                return _lastGuessedNumber;
            }

        }
        #endregion 

        #region Methods

        public int? randomNumber()
        {
            Random rnd = new Random();
            _number = 0;
            _number = rnd.Next(minNumber, maxNumber);
            return _number;
        }

        public void Initialize()
        {
            Number = randomNumber();
            if (GuessedNumbers.Count > 0)
            {
                GuessedNumbers.Clear(); 
            }
        }
        
        public Outcome MakeGuess(int guess)
        {
            GuessedNumber newGuess = new GuessedNumber();
            newGuess.Number = guess;

            if (guess < 1 || guess > 100)
            {
                throw new ArgumentOutOfRangeException(String.Empty, "Du måste ange ett värde mellan 1 och 100");
            }

            if (guess < _number)
            {
                newGuess.Outcome = Outcome.Low;
            }

            if (guess > _number)
            {
                newGuess.Outcome = Outcome.High;
            }

            if (guess == _number)
            {
                newGuess.Outcome = Outcome.Right;
            }

            if (!CanMakeGuess)
            {
                newGuess.Outcome = Outcome.NoMoreGuesses;
            }
            foreach (GuessedNumber number in GuessedNumbers)
            {
                if (number.Number == guess)
                {
                    newGuess.Outcome = Outcome.OldGuess;
                }
            }

            if (newGuess.Outcome != Outcome.OldGuess) { 
            _guessedNumbers.Add(newGuess);
            }
            _lastGuessedNumber = newGuess;
            return newGuess.Outcome;
        }
        #endregion

        //Constructor
        public SecretNumber()
        {  
            _guessedNumbers = new List<GuessedNumber>(); 
            Initialize();
        }
    }
}