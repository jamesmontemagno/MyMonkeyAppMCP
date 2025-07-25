using System.Collections.Generic;

namespace MonkeyApp
{
    public static class MonkeyFacts
    {
        private static readonly HashSet<int> _shownFactIndexes = new();
        private static readonly Random _random = new();

        private static readonly List<string> _facts = new()
        {
            "Monkeys can live for up to 50 years in captivity!",
            "Some monkeys can swim, including proboscis monkeys and macaques.",
            "Monkeys have excellent color vision and can see in full color.",
            "A group of monkeys is called a 'troop' or 'barrel'.",
            "Monkeys are one of the few animals that can recognize themselves in mirrors.",
            "Capuchin monkeys use tools like stones to crack open nuts and shells.",
            "Howler monkeys are the loudest land animals - their calls can be heard 3 miles away!",
            "Spider monkeys don't have thumbs, which helps them swing through trees faster.",
            "Some monkeys in Thailand train macaques to harvest coconuts from tall palm trees.",
            "Monkeys have been sent to space! A rhesus monkey named Albert II was the first in 1949.",
            "Japanese macaques enjoy relaxing in hot springs during winter.",
            "Vervet monkeys have different alarm calls for different types of predators.",
            "Golden lion tamarins were once nearly extinct but conservation efforts saved them.",
            "Mandrill monkeys have the most colorful faces in the animal kingdom.",
            "Some monkeys can learn sign language and communicate with humans!",
            "Monkeys spend about 8 hours a day foraging for food.",
            "Baby monkeys cling to their mothers for the first few weeks of life.",
            "Monkeys are highly social animals and form complex relationships.",
            "The smallest monkey is the pygmy marmoset, weighing only 4 ounces.",
            "Monkeys have been observed using medicinal plants when they're sick."
        };

        public static string GetRandomFact()
        {
            // If all facts have been shown, reset the tracking
            if (_shownFactIndexes.Count >= _facts.Count)
            {
                _shownFactIndexes.Clear();
            }

            // Find an unshown fact
            int factIndex;
            do
            {
                factIndex = _random.Next(_facts.Count);
            } while (_shownFactIndexes.Contains(factIndex));

            // Mark this fact as shown
            _shownFactIndexes.Add(factIndex);

            return _facts[factIndex];
        }

        public static int GetShownFactsCount()
        {
            return _shownFactIndexes.Count;
        }

        public static int GetTotalFactsCount()
        {
            return _facts.Count;
        }
    }
}