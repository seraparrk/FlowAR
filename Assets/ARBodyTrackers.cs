using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARBodyTrackers : MonoBehaviour
{
    [SerializeField] //Inspector 창에 노출
    GameObject bodyPrefab; //GameObject이름 'bodyprefab'으로 설정

    [SerializeField] 
    Vector3 offset; //벡터데이터이름 'offset'으로 설정

    ARHumanBodyManager humanBodyManager; //BodyTracking하는 컴포넌트 클래스 추가
   
    GameObject bodyObject;  //bodyPrefab 인스턴스화해서 저장할 GameObject이름 설정

    //Awake함수 만들어 컴포넌트 데이터 받아오기
    void Awake()
    {
        humanBodyManager = (ARHumanBodyManager) GetComponent<ARHumanBodyManager>();
    }
    
    //이벤트함수 등록 및 해제
    private void OnEnable(){
        humanBodyManager.humanBodiesChanged += OnHumanBodiesChanged;
    }
    
    private void OnDisable(){
        humanBodyManager.humanBodiesChanged -= OnHumanBodiesChanged;
    }

    //이벤트 전달받기 위한 함수만들기
    void OnHumanBodiesChanged(ARHumanBodiesChangedEventArgs eventArgs)
    {
        //humanBody 추가 되었을 때
        foreach (ARHumanBody humanBody in eventArgs.added)
        {
            //humanBody에 bodyObject 인스턴스화 
            bodyObject = Instantiate(bodyPrefab, humanBody.transform);
        }

        foreach(ARHumanBody humanBody in eventArgs.updated)
        {
            if(bodyObject != null)
            {
                //humanBody에 bodyObject위치 설정
                bodyObject.transform.position = humanBody.transform.position + offset;
                bodyObject.transform.rotation = humanBody.transform.rotation;
                bodyObject.transform.localScale = humanBody.transform.localScale;
            }

        }

        //humanBody 없어졌을 때
        foreach(ARHumanBody hunmanbody in eventArgs.removed)
        {
            if(bodyObject != null)
            {
                //bodyObjcet 없애기
                Destroy(bodyObject);
            }

        }
    }


}
