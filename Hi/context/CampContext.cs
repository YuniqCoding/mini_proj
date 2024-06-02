using System;
using camp.data;
using camp.model;
using camp.repository;

namespace camp
{
	public class CampContext
	{
        public StudentService StudentServ { get; }
        public SubjectService SubjectServ { get; }
        public ScoreService ScoreServ { get; }

        public CampContext()
        {
            StudentStore studentStore = new StudentStore();
            SubjectStore subjectStore = new SubjectStore();
            ScoreStore scoreStore = new ScoreStore();
            StudentServ = new StudentService(studentStore);
            SubjectServ = new SubjectService(subjectStore);
            ScoreServ = new ScoreService(scoreStore, new MandatoryPolicy(), new ChoicePolicy());
            new DummyData(studentStore, subjectStore, ScoreServ).InitDummyData();
        }
    }
}

