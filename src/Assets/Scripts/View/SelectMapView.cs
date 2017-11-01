using strange.extensions.mediation.impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectMapView : View {

    public MapView[] mapViews;

    public Button btn_back;

    public void GetMapView() {
        mapViews = GameObject.FindObjectsOfType<MapView>();
    }
}
