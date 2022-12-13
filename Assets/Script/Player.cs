using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    [SerializeField] private AudioClip _moveClip, _loseClip, _pointClip;

    [SerializeField] private GameplayManager _gm;
    [SerializeField] private GameObject _explosionPrefab, _scoreParticlePrefab;

    //  public static bool GetButtonDown(string buttonName); 




    private bool canClick;

    private void Awake()
    {
        canClick = true;
        level = 0;
        currentRadius = _startRadius;
    }

    private void Update()
    {
       /* if (canClick && Input.GetMouseButtonDown(0))
        {
            StartCoroutine(ChangeRadius());
            if(_moveClip!=null)
            SoundManager.Instance.PlaySound(_moveClip);
        }*/
        
            //check is space key pressed down 
             
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentRadius = _rotateRadius[level];
            level = (level + 1);
            level = Mathf.Clamp(level, 0, 2);
            currentRadius = _rotateRadius[level];

        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            level = (level - 1);
            level = Mathf.Clamp(level, 0, 0);
            currentRadius = _rotateRadius[level];
        }


    }

    [SerializeField] private float _rotateSpeed;
    [SerializeField] private Transform _rotateTransform;

    private void FixedUpdate()
    {
        transform.localPosition = Vector3.up * currentRadius;
        float rotateValue = _rotateSpeed * Time.fixedDeltaTime * _startRadius / currentRadius;
        _rotateTransform.Rotate(0, 0, rotateValue);
    }
     
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        { 
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            if(_loseClip!=null)
            SoundManager.Instance.PlaySound(_loseClip);
            _gm.GameEnded();
            return;
        }

        if (collision.CompareTag("Score"))
        {
            Destroy(Instantiate(_scoreParticlePrefab, transform.position, Quaternion.identity), 1f);
            if (_pointClip)
            {
                SoundManager.Instance.PlaySound(_pointClip);
            }
            _gm.UpdateScore();
           // collision.gameObject.GetComponent<Score>().ScoreAdded();
            return;
        }
    }


    [SerializeField] private float _startRadius;
    [SerializeField] private float _moveTime;

    [SerializeField] private List<float> _rotateRadius;
    [SerializeField] float up, down;
    private float currentRadius;

    private int level;


  /* private IEnumerator ChangeRadius()

   {

        if (up)
        {
            if (!Endlevel)
            {
                level = (level + 1) % _rotateRadius.Count;
                currentRadius = _rotateRadius[level1];
            }
        }*/


        // this  method change radius in up down click





        /*
         *  this change radius of click on mouse 
         *  
         * 
         canClick = false;
        float moveStartRadius = _rotateRadius[level];
        float moveEndRadius = _rotateRadius[(level + 1) % _rotateRadius.Count];
        float moveOffset = moveEndRadius - moveStartRadius;
        float speed = 1 / _moveTime;
        float timeElasped = 0f;
        while (timeElasped < 1f)
        {
            timeElasped += speed * Time.fixedDeltaTime;
            currentRadius = moveStartRadius + timeElasped * moveOffset;
            yield return new WaitForFixedUpdate();
        }

        canClick = true;
        level = (level + 1) % _rotateRadius.Count;
        currentRadius = _rotateRadius[level];
        */
   // }
    

}