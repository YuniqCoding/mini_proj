using System;
using camp.enums;
using camp.model;
using camp.repository;

namespace camp
{
    public class SubjectService
    {
        private readonly SubjectStore subjectStore;

        public SubjectService(SubjectStore subjectStore)
        {
            this.subjectStore = subjectStore;
        }
        public List<Subject> InquireSubjects()
        {
            return subjectStore.Store;
        }

        public List<Subject> SelectSubjects(string inputSubjects)
        {
            List<Subject> selectedSubjects = new List<Subject>();
            List<string> subjectIds = inputSubjects.Trim().Replace(" ", "").Split(',').ToList();

            foreach (Subject subject in subjectStore.Store)
            {
                if (subjectIds.Contains(subject.SubjectId))
                {
                    selectedSubjects.Add(subject);
                }
            }
            return selectedSubjects;
        }

        public bool ValidateSelectSubjects(List<Subject> selectSubjects)
        {
            int mandatoryCount = 0;
            int choiceCount = 0;
            foreach (Subject selectSubject in selectSubjects)
            {
                if (SubjectType.MANDATORY == selectSubject.SubjectOption)
                {
                    mandatoryCount++;
                }
                else
                {
                    choiceCount++;
                }
            }
            return mandatoryCount >= 3 && choiceCount >= 2;
        }

        public Subject FindSubjectById(string subjectId)
        {
            // subjectStore에서 모든 과목을 순회하며 ID를 검사합니다.
            foreach (Subject subject in subjectStore.Store)
            {
                if (subject.SubjectId == subjectId)
                {
                    return subject;
                }
            }

            // 해당 ID의 과목을 찾지 못한 경우 예외를 발생시킵니다.
            throw new Exception("Not Found Subject");
        }

    }
}

