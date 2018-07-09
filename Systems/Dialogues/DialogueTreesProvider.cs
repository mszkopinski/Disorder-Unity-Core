//using System;
//using System.Collections.Generic;
//using System.Linq;
//using UnityEngine;

//namespace Disorder.TraderSimulator.Core
//{
//    //TODO remember that you are using Application.dataPath temporary. It won't work in production
//    public class DialogueTreesProvider
//    {
//        const string FileName = "dialogue.xml"; // later file should be moved to static resources or repository

//        readonly IXmlSerializationProvider xmlSerializationProvider;
//        readonly List<DialogueTreeData> _dialogueTreesData;

//        public DialogueTreesProvider(IXmlSerializationProvider xmlSerializationProvider)
//        {
//            this.xmlSerializationProvider = xmlSerializationProvider;
//            _dialogueTreesData = new List<DialogueTreeData>();
//        }

//        public void FetchDialogueTreesData()
//        {
//            var data = FetchTreeFromXml(FileName);

//            if (data == null)
//                return;

//            foreach (var dialogueTree in data.DialogueTrees)
//            {
//                if (_dialogueTreesData.Contains(dialogueTree))
//                    return;

//                _dialogueTreesData.Add(dialogueTree);
//            }

//            OnDialoguesFetched();
//        }

//        DialoguesTreesListData FetchTreeFromXml(string fileName)
//        {
//            return (DialoguesTreesListData)xmlSerializationProvider
//                .DeserializeObject<DialoguesTreesListData>(
//                    xmlSerializationProvider.LoadDataFromXml(Application.dataPath, fileName));
//        }

//        public void SaveTreeToXml(DialogueTree tree, string fileName)
//        {
//            string dataToWrite = xmlSerializationProvider.SerializeObject<DialogueTreeData>(tree);
//            xmlSerializationProvider.CreateXmlFileOutput(Application.dataPath, fileName, dataToWrite);
//        }

//        #region Getters

//        public DialogueTree GetTreeById(int id)
//        {
//            var wantedTree = _dialogueTreesData
//                .Select(treeData => new DialogueTree(treeData)).First(tree => tree.Id == id);

//            return wantedTree ?? new DialogueTree();
//        }

//        public DialogueTree GetTreeByCategory(string dialogueCategory)
//        {
//            var wantedTree = _dialogueTreesData
//                .Select(dialogueData => new DialogueTree(dialogueData)).First(tree => tree.DialogueTreeCategory == dialogueCategory);

//            return wantedTree ?? new DialogueTree();
//        }

//        public DialogueNode GetNodeById(int treeId, int nodeId)
//        {
//            var wantedNode = _dialogueTreesData
//                .Select(treeData => new DialogueTree(treeData))
//                .First(tree => tree.Id == treeId).DialogueNodes
//                .First(node => node.Id == nodeId);

//            return wantedNode ?? new DialogueNode();
//        }

//        #endregion

//        #region Events

//        public EventHandler DialoguesTreesFetched;

//        protected void OnDialoguesFetched()
//        {
//            DialoguesTreesFetched?.Invoke(this, System.EventArgs.Empty);
//        }

//        #endregion
//    }
//}