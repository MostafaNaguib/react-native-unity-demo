using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace EnjoyLearning.VR.SDK
{
    public class CardboardButtonSimulator : MonoBehaviour
    {
        public VRModeManager vrModeManager;

        public GameObject gvrRectile;
        public MeshRenderer gvrMeshRenderer;

        public Image innerLoadingImage;
        public Image outerLoadingImage;
        public int loadingSteps;
        public float buttonPressWait = 1.0f;
        public float refreshWait = 0.1f;

        private Coroutine butttonSimulatorCoroutine;

        [SerializeField] 
		private GameObject target;

        private void OnEnable()
        {
            CardboardButtonTarget.OnTargetSelected += OnTargetSelected;
            CardboardButtonTarget.OnTargetDeSelected += OnTargetDeSelected;
        }

        private void OnDisable()
        {
            CardboardButtonTarget.OnTargetSelected -= OnTargetSelected;
            CardboardButtonTarget.OnTargetDeSelected -= OnTargetDeSelected;
        }

        IEnumerator SimulatingButton()
        {
            float fillStep = 1.0f / loadingSteps;
            float wait = buttonPressWait / loadingSteps;

            if (vrModeManager.vrMode)
            {
                gvrMeshRenderer.enabled = false;
            }

            for (int j = 0; j < loadingSteps; j++)
            {
                innerLoadingImage.fillAmount += fillStep;
                outerLoadingImage.fillAmount += fillStep;
                yield return new WaitForSeconds(wait);
            }

            innerLoadingImage.fillAmount = 1.0f;
            outerLoadingImage.fillAmount = 1.0f;

            yield return new WaitForSeconds(refreshWait);
            ExecuteEvents.Execute<IPointerDownHandler>(target, new PointerEventData(EventSystem.current), ExecuteEvents.pointerDownHandler);
            yield return new WaitForSeconds(refreshWait);
            ExecuteEvents.Execute<IPointerUpHandler>(target, new PointerEventData(EventSystem.current), ExecuteEvents.pointerUpHandler);
            yield return new WaitForSeconds(refreshWait);
            ExecuteEvents.Execute<IPointerClickHandler>(target, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);

            gvrRectile.SetActive(false);

            OnTargetDeSelected();
        }

        private void OnTargetSelected(GameObject gameObject)
        {
            target = gameObject;

            // make sure that if the target is ui, it is also interactable
            var uiTarget = target.GetComponent<Selectable>();
            if (uiTarget != null && !uiTarget.interactable)
            {
                //Debug.LogFormat("Target: {0} is not interactable", target.name);
                return;
            }

            //Debug.LogFormat("Activating Simulator by Target: {0} of type {1}", target.name, target.GetType());
            butttonSimulatorCoroutine =  StartCoroutine(SimulatingButton());
        }

        private void OnTargetDeSelected()
        {
            if (butttonSimulatorCoroutine != null)
                StopCoroutine(butttonSimulatorCoroutine);

            innerLoadingImage.fillAmount = 0.0f;
            outerLoadingImage.fillAmount = 0.0f;

            butttonSimulatorCoroutine = null;
            target = null;

            if(gvrRectile != null)
                gvrRectile.SetActive(true);

            if (vrModeManager.vrMode && gvrMeshRenderer != null)
            {
                gvrMeshRenderer.enabled = true;
            }
        }
    }
}

