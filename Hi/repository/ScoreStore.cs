using System;
using System.Collections.Generic;
using camp.context;
using camp.model;

namespace camp.repository
{
    public class ScoreStore
    {
        public List<Score> Store { get; }
        private int storeIndex;

        public ScoreStore()
        {
            Store = new List<Score>();
        }

        private string Sequence()
        {
            storeIndex++;
            return Constants.IndexType.SCORE + storeIndex;
        }

        public Score Save(Score score)
        {
            score.ScoreId = Sequence();
            Store.Add(score);
            return score;
        }

        public void DeleteAll(List<Score> deleteScoreList)
        {
            Store.RemoveAll(deleteScoreList.Contains);
        }
    }
}