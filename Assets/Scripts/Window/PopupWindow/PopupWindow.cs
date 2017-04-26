using UnityEngine;
using System.Collections;

public class PopupWindow : MonoBehaviour {

    public UILabel LabelMessage;
    public TweenScale TweenBg;
    public TweenScale TweenMessage;
    public GameObject JoinButton;
    public GameObject RedButton;

    private int[] Levellimit = new int[] { 0, 20, 30, 40, 50, 60, 70 };

    int RoomNum = 0;
	void Start () {
	
	}
/*
    public void SetLabelOneMessage(string Message) 
    {
        //LabelMessage.text = Message;
        //TweenBg.from = new Vector3(1f, 0, 0);
        //TweenBg.to = new Vector3(1f, 1f, 0);
        //TweenBg.PlayForward();

        //TweenMessage.from = new Vector3(1f, 0, 0);
        //TweenMessage.to = new Vector3(1f, 1f, 0);
        //TweenMessage.PlayForward();    
        this.gameObject.transform.localPosition = new Vector3(0, 200f, 0);
        StartCoroutine(LabelOneMessage(Message));
    }

    IEnumerator LabelOneMessage(string Message) 
    {
        LabelMessage.text = Message;
        TweenBg.from = new Vector3(1f, 0, 0);
        TweenBg.to = new Vector3(1f, 1f, 0);
        TweenBg.PlayForward();
        yield return new WaitForSeconds(1f);

        TweenMessage.from = new Vector3(1f, 0, 0);
        TweenMessage.to = new Vector3(1f, 1f, 0);
        TweenMessage.PlayForward();
        yield return new WaitForSeconds(4.2f);

        TweenBg.from = new Vector3(1f, 1, 0);
        TweenBg.to = new Vector3(1f, 0f, 0);
        TweenBg.ResetToBeginning();
        TweenBg.PlayForward();
        yield return new WaitForSeconds(1.2f);
        DestroyImmediate(this.gameObject);
    }

    public void SetLabelMoreMessage() 
    {
        this.gameObject.transform.localPosition = new Vector3(0, 200f, 0);
        StartCoroutine(LabelMoreMessage());
    }
    IEnumerator LabelMoreMessage() 
    {
        TweenBg.from = new Vector3(1f, 0, 0);
        TweenBg.to = new Vector3(1f, 1f, 0);
        TweenBg.PlayForward();
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < TextTranslator.instance.PopupMessage.Count; i++) 
        {
            LabelMessage.text = TextTranslator.instance.PopupMessage[i];

            TweenMessage.from = new Vector3(1f, 0, 0);
            TweenMessage.to = new Vector3(1f, 1f, 0);
            TweenMessage.ResetToBeginning();
            TweenMessage.PlayForward();
            yield return new WaitForSeconds(4.2f);
        }

        TweenBg.from = new Vector3(1f, 1, 0);
        TweenBg.to = new Vector3(1f, 0f, 0);
        TweenBg.ResetToBeginning();
        TweenBg.PlayForward();
        yield return new WaitForSeconds(1.2f);
        DestroyImmediate(this.gameObject);
    }

    */

    //***************
    /// <summary>
    /// 
    /// </summary>
    public void SetlabelmoreMessage()   //组队或者军团邀请用
    {
        StartCoroutine(labelmoreMessage());
    }
    IEnumerator labelmoreMessage() 
    {
        TweenBg.from = new Vector3(1f, 0, 0);
        TweenBg.to = new Vector3(1f, 1f, 0);
        TweenBg.PlayForward();
        yield return new WaitForSeconds(1f);

        while (TextTranslator.instance.IsQueueAvailable)
        {
            string[] dataSplit = TextTranslator.instance.ReadQueuedoc().Split(';');
            LabelMessage.text = dataSplit[4];
            if (dataSplit[0] == "10") //组队邀请
            {
                JoinButton.SetActive(true);
                JoinButton.transform.GetComponent<BoxCollider>().enabled = true;
                RoomNum = int.Parse(dataSplit[3]);
                UIEventListener.Get(JoinButton).onClick = delegate(GameObject go)
                {
                    JoinButton.transform.GetComponent<BoxCollider>().enabled = false;
                    int level = TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.zuduifuben).Level;
                    if (CharacterRecorder.instance.lastGateID < level)
                    {
                        UIManager.instance.OpenPromptWindow((level-10000).ToString() + "关开放组队副本", PromptWindow.PromptType.Hint, null, null);
                    }
                    else if (GameObject.Find("FightWindow") != null || GameObject.Find("LegionFightWindow") != null || GameObject.Find("KingRoadFightWindow") != null || GameObject.Find("WorldBossFightWindow") != null || 
                              GameObject.Find("RoleWindow") != null || GameObject.Find("StrengEquipWindow") != null || GameObject.Find("GaChaGetWindow") != null || GameObject.Find("TeamInvitationWindow") != null ||
                              GameObject.Find("TeamFightChoseWindow") != null || GameObject.Find("MapUiWindow")!=null)
                    {
                        UIManager.instance.OpenPromptWindow("当前场景不可参加组队", PromptWindow.PromptType.Hint, null, null);
                    }
                    else
                    {
                        NetworkHandler.instance.SendProcess("6108#;");
                        NetworkHandler.instance.SendProcess("6109#" + RoomNum + ";");
                    }
                };
                IsAutoEnterCopyWindow();
            }
            else if (dataSplit[0] == "41") //抢红包
            {
                RedButton.SetActive(true);
                UIEventListener.Get(RedButton).onClick = delegate(GameObject go)
                {
                    NetworkHandler.instance.SendProcess(string.Format("9521#{0};", dataSplit[2]));
                };
            }

            TweenMessage.from = new Vector3(1f, 0, 0);
            TweenMessage.to = new Vector3(1f, 1f, 0);
            TweenMessage.ResetToBeginning();
            TweenMessage.PlayForward();
            yield return new WaitForSeconds(4.2f);
            JoinButton.SetActive(false);
            RedButton.SetActive(false);
        }

        TweenBg.from = new Vector3(1f, 1, 0);
        TweenBg.to = new Vector3(1f, 0f, 0);
        TweenBg.ResetToBeginning();
        TweenBg.PlayForward();
        yield return new WaitForSeconds(1.2f);        
        DestroyImmediate(this.gameObject);
    }


    /// <summary>
    /// 抢红包按钮特殊用
    /// </summary>

    public void SetRedlabelmoreMessage()   
    {
        StartCoroutine(RedlabelmoreMessage());
    }
    IEnumerator RedlabelmoreMessage()
    {
        TweenBg.from = new Vector3(1f, 0, 0);
        TweenBg.to = new Vector3(1f, 1f, 0);
        TweenBg.PlayForward();
        yield return new WaitForSeconds(1f);

        while (TextTranslator.instance.IsQueueAvailable)
        {
            LabelMessage.text = TextTranslator.instance.ReadQueuedoc();
            if (LabelMessage.text.Contains("："))
            {
                JoinButton.SetActive(true);
                string[] Roomstr = LabelMessage.text.Split('：');
                UIEventListener.Get(JoinButton).onClick = delegate(GameObject go)
                {

                };
                RoomNum = int.Parse(Roomstr[1]);
                IsAutoEnterCopyWindow();
            }
            TweenMessage.from = new Vector3(1f, 0, 0);
            TweenMessage.to = new Vector3(1f, 1f, 0);
            TweenMessage.ResetToBeginning();
            TweenMessage.PlayForward();
            yield return new WaitForSeconds(4.2f);
            JoinButton.SetActive(false);
        }

        TweenBg.from = new Vector3(1f, 1, 0);
        TweenBg.to = new Vector3(1f, 0f, 0);
        TweenBg.ResetToBeginning();
        TweenBg.PlayForward();
        yield return new WaitForSeconds(1.2f);
        DestroyImmediate(this.gameObject);
    }










    void IsAutoEnterCopyWindow() //是否自动进入组队
    {
        if(GameObject.Find("TeamCopyWindow")!=null)
        {
            GameObject.Find("TeamCopyWindow").GetComponent<TeamCopyWindow>().IsAutoEnterCopyWindow(RoomNum);
        }
    }


    public void JoinTeamCondition(TeamBrowseItemDate _oneTeamBrowsItemDate)//查找组队信息
    {
        int level = TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.zuduifuben).Level;
        if (CharacterRecorder.instance.lastGateID >= level)
        {
            StartCoroutine(StatyTime(_oneTeamBrowsItemDate));
        }
        else
        {
            UIManager.instance.OpenPromptWindow("通过" + (level-10000).ToString()+ "关解锁组队副本", PromptWindow.PromptType.Hint, null, null);
        }
    }
    IEnumerator StatyTime(TeamBrowseItemDate _oneTeamBrowsItemDate)
    {
        Levellimit = new int[] { 0, 20, 30, 40, 50, 60, 70 };

        yield return new WaitForSeconds(0.6f);
        string[] trcSplit = _oneTeamBrowsItemDate.condition1.Split('-');

        int OpenLevel = 0;
        for (int i = 0; i < TextTranslator.instance.TeamGateList.size; i++)
        {
            if (TextTranslator.instance.TeamGateList[i].GroupID == _oneTeamBrowsItemDate.copyNumber)
            {
                Debug.Log("关卡开放等级" + TextTranslator.instance.TeamGateList[i].NeedLevel);
                OpenLevel = TextTranslator.instance.TeamGateList[i].NeedLevel;
                break;
            }
        }
        Debug.Log("此队伍需要的等级" + Levellimit[int.Parse(_oneTeamBrowsItemDate.condition3)]);
        Debug.Log("帮打时间" + CharacterRecorder.instance.TeamHelpTime);

        if (CharacterRecorder.instance.level < Levellimit[int.Parse(_oneTeamBrowsItemDate.condition3)])
        {
            UIManager.instance.OpenPromptWindow("房间等级限制，无法加入", PromptWindow.PromptType.Hint, null, null);
        }
        else if (trcSplit[0] != "0")
        {
            if (trcSplit[0] == "2")
            {
                if (!CharacterRecorder.instance.MyFriendUIDList.Contains(int.Parse(trcSplit[1])))
                {
                    UIManager.instance.OpenPromptWindow("仅限队长好友可加入", PromptWindow.PromptType.Hint, null, null);
                }
                else 
                {
                    NetworkHandler.instance.SendProcess("6103#" + _oneTeamBrowsItemDate.teamId + ";");
                }
            }
            else
            {
                if (trcSplit[1] != "0" && CharacterRecorder.instance.legionID.ToString() != trcSplit[1])
                {
                    UIManager.instance.OpenPromptWindow("仅限队长同军团成员可加入", PromptWindow.PromptType.Hint, null, null);
                }
                else 
                {
                    NetworkHandler.instance.SendProcess("6103#" + _oneTeamBrowsItemDate.teamId + ";");
                }
            }
        }
        else if (CharacterRecorder.instance.TeamHelpTime > 0 && CharacterRecorder.instance.TeamFightNum == 0)
        {
            UIManager.instance.OpenPromptWindow("还在CD时间", PromptWindow.PromptType.Hint, null, null);
        }
        else if (CharacterRecorder.instance.level < OpenLevel)
        {
            UIManager.instance.OpenPromptWindow("等级不足，尚未解锁该难度", PromptWindow.PromptType.Hint, null, null);
        }
        else if (_oneTeamBrowsItemDate.teamstate == 1)
        {
            UIManager.instance.OpenPromptWindow("副本进行中", PromptWindow.PromptType.Hint, null, null);
        }
        else
        {
            NetworkHandler.instance.SendProcess("6103#" + _oneTeamBrowsItemDate.teamId + ";");
        }
    }


    //IEnumerator StatyTime(TeamBrowseItemDate _oneTeamBrowsItemDate)
    //{
    //    yield return new WaitForSeconds(0.2f);
    //    string[] trcSplit = _oneTeamBrowsItemDate.condition1.Split('-');

    //    if (CharacterRecorder.instance.level < Levellimit[int.Parse(_oneTeamBrowsItemDate.condition3)])
    //    {
    //        UIManager.instance.OpenPromptWindow("等级不足", PromptWindow.PromptType.Hint, null, null);
    //    }
    //    else if (trcSplit[0] != "0")
    //    {
    //        if (trcSplit[0] == "2")
    //        {
    //            if (!CharacterRecorder.instance.MyFriendUIDList.Contains(int.Parse(trcSplit[1])))
    //            {
    //                UIManager.instance.OpenPromptWindow("不是好友,无法加入房间", PromptWindow.PromptType.Hint, null, null);
    //            }
    //        }
    //        else
    //        {
    //            if (trcSplit[1] != "0" && CharacterRecorder.instance.legionID.ToString() != trcSplit[1])
    //            {
    //                UIManager.instance.OpenPromptWindow("不是同一军团,无法加入房间", PromptWindow.PromptType.Hint, null, null);
    //            }
    //        }
    //    }
    //    else if (CharacterRecorder.instance.TeamHelpTime > 0 && CharacterRecorder.instance.TeamFightNum==0)
    //    {
    //        UIManager.instance.OpenPromptWindow("还在CD时间", PromptWindow.PromptType.Hint, null, null);
    //    }
    //    else
    //    {
    //        NetworkHandler.instance.SendProcess("6103#" + _oneTeamBrowsItemDate.teamId + ";");
    //    }
    //}
}

/*
 //c#中，如果多个线程访问同一个方法或同一个类，可以用lock(this)的方法来锁定，如果是访问同一个对象，则应该用monitor加queue来进行监控


using System.Text;
using System.Threading;
namespace ConsoleApplication1
{
    class Program
    {
        //static void Main(string[] args)
        //{
        //}
        public static void Main() 
        {  
            //队列: 元素以先进先出的方式来处理的集合(FIFO-First In First Out)第一个来,第一个滚 
            //例:飞机登记排队,靠前的就先上飞要,不过队列有优先级,如商务仓的队与经济仓的队,是两个不同的队,而商务优先 
            //在.Net技术中,      using System.Collections.Generic.Queue<T>是队列的泛型版本实现 
            // System.Collections.Queue是非泛型的实现,参数是Object 
            //public class Queue<T> : IEnumerable<T>, ICollection, IEnumerable 
            //从队列的定义可以看出,它实现了跌代,集合接口 ,它没有实现ICollection<T>这个接口,因为其中定义的Add Remove方法会破坏队列 
            //队列与列表主要区别就是在于,它没有实现IList接口 
            //所以,不可以用索引器访问队列,队列只允许添加元素,该元素只能放在队列的最后(Enqueue()),还有就是从头部取元素Dequeue() 
            //Enqueue从队列的后面插入元素,而 Dequeue在取一个元素的同时,会前取出的元素删除,如再再调用一次,就删除下一个元素 
             
            //方法简介 : 
            // Enqueue() 一端插入元素 
            // Dequeue() 从头部读取和删除一个元素,如果调用方法时,队列中没有元素,刨出InvalidOperationException异常 
            // Peek() 在队列头部读取一个元素,但是,不删除它 
            // Count 返回队列元素个数 
            // TrimExcess() 重新设置队列容量,Dequeue方法,是可能删除一个对象,但是,不会重设容量,这样会浪费空余空间 ,本方法,从头部清除空余空间 
            // Contains() 确定某个元素是不是在队列中,如果是,返回true 
            // CopyTo() 把元素从队列复制到一个已有的数组中  
            // ToArray() 返回一个包含队列元素的新数组 

            // 下面我们开始写一个关于队列的例子, 使用一个线程将文档添加到队列中,用另一个线程读取队列,并处理他们 
            // 存储队列的类型是Document,我们先定义一个文档类,接着定义一个文档处理类DocumentManager,其中包含了添加与读取方法,还有一个属性确定队列中是不是为空 
            // 然后我们定义一个 ProcessDocuments来处理线程,操作文档 
            
            DocumentManager mg = new DocumentManager(); 
            ProcessDocuments prcess = new ProcessDocuments( mg ); 
            //启动读取线程，不过现在没内容，要等一会加入后，就能读到了 
            ProcessDocuments.Start(mg); 

            Document doc = null; 

            for (int i = 0; i < 500; i++) 
            { 
                doc = new Document("Aladdin:" + i, "Hello,Nihao!"); 
                mg.AddDocument( doc ); 
                //睡会，让给其他线程玩会 
                //Thread.Sleep(20); 
            } 

            Console.ReadLine(); 
        } 
    } 
     
    // 文档类,描述了文档的标题与内容 
    class Document 
    { 
        public string title; 
        public string content; 

        public Document(string title, string content) 
        { 
            this.title = title; 
            this.content = content; 
        } 
    } 

    class DocumentManager 
    { 
        //定义队列集合 
        private readonly Queue<Document> docQueue = new Queue<Document>(); 
         
        //添加文档 
        public void AddDocument(Document doc) 
        { 
            lock (this) 
            { 
                //从队列一端插入内容 
                docQueue.Enqueue(doc); 
                Console.WriteLine( "成功插入文档:" + doc.title); 
            } 
        } 
         
        //读取文档 
        public Document GetDocument() 
        { 
            Document doc = null; 

            lock (this) 
            { 
                doc = docQueue.Dequeue(); 
                return doc; 
            } 
        } 

        //只读属性,确定队列中是不是还有元素 
        public bool IsDocumentAvailable 
        { 
            get 
            { 
                return docQueue.Count > 0; 
            } 
        } 
    } 

    // 处理文档 
    class ProcessDocuments 
    { 
        private DocumentManager dm; 

        public ProcessDocuments( DocumentManager dm ) 
        { 
            this.dm = dm; 
        } 

        public static void Start(DocumentManager manager) 
        {  
            // 参数 ： public delegate void ThreadStart(); 
            new Thread(new ProcessDocuments(manager).DoThreadFunc).Start() ; 
        } 

        public void DoThreadFunc() 
        {  
            //无限循环读取，只要队列中有内容，这条线程就读出来 

            while (true) 
            { 
                if (this.dm.IsDocumentAvailable) 
                { 
                    Document doc = this.dm.GetDocument() ; 
                    Console.WriteLine("从队列中读到读取并删除文档 标题：{0}   内容： {1}", doc.title, doc.content ); 
                } 
            } 
        } 
    }
}
*/

