using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using UnityEditor;

public class EditorMenus {

	// LOAD FASES //

	[MenuItem("Fases/Menu %&F1", false, 1)]
	private static void NewMenuMenu() {

		if (!EditorApplication.isPlaying) {

			EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo ();
			EditorSceneManager.OpenScene ("Assets/Scenes/Cenas do Jogo/Menu.unity");
		} else {

			SceneManager.LoadScene ("Menu");
		}
	}

	[MenuItem("Fases/Tutorial %#F1", false, 51)]
	private static void NewMenuOption() {

		if (!EditorApplication.isPlaying) {

			EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo ();
			EditorSceneManager.OpenScene ("Assets/Scenes/Cenas do Jogo/Z1F0.unity");
		} else {

			SceneManager.LoadScene ("Z1F0");
		}
	}

	[MenuItem("Fases/Z1F1 %F1", false, 101)]
	private static void NewMenuOption2() {

		if (!EditorApplication.isPlaying) {
			
			EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo ();
			EditorSceneManager.OpenScene ("Assets/Scenes/Cenas do Jogo/Z1F1.unity");
		} else {

			SceneManager.LoadScene ("Z1F1");
		}
	}

	[MenuItem("Fases/Z1F2 %F2", false, 102)]
	private static void NewMenuOption3() {

		if (!EditorApplication.isPlaying) {
			
			EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo ();
			EditorSceneManager.OpenScene ("Assets/Scenes/Cenas do Jogo/Z1F2.unity");
		} else {

			SceneManager.LoadScene ("Z1F2");
		}
	}

	[MenuItem("Fases/Z1F3 %F3", false, 103)]
	private static void NewMenuOption4() {

		if (!EditorApplication.isPlaying) {

			EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo ();
			EditorSceneManager.OpenScene ("Assets/Scenes/Cenas do Jogo/Z1F3.unity");
		} else {

			SceneManager.LoadScene ("Z1F3");
		}
	}

	[MenuItem("Fases/Z2F1 %F4", false, 151)]
	private static void NewMenuOption5() {

		if (!EditorApplication.isPlaying) {

			EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo ();
			EditorSceneManager.OpenScene ("Assets/Scenes/Cenas do Jogo/Z2F1.unity");
		} else {

			SceneManager.LoadScene ("Z2F1");
		}
	}

	[MenuItem("Fases/Z2F2 %F5", false, 152)]
	private static void NewMenuOption6() {

		if (!EditorApplication.isPlaying) {

			EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo ();
			EditorSceneManager.OpenScene ("Assets/Scenes/Cenas do Jogo/Z2F2.unity");
		} else {

			SceneManager.LoadScene ("Z2F2");
		}
	}

	[MenuItem("Fases/Z2F3 %F6", false, 153)]
	private static void NewMenuOption7() {

		if (!EditorApplication.isPlaying) {

			EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo ();
			EditorSceneManager.OpenScene ("Assets/Scenes/Cenas do Jogo/Z2F3.unity");
		} else {

			SceneManager.LoadScene ("Z2F3");
		}
	}

	[MenuItem("Fases/Z3F1 %F7", false, 201)]
	private static void NewMenuOption8() {

		if (!EditorApplication.isPlaying) {

			EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo ();
			EditorSceneManager.OpenScene ("Assets/Scenes/Cenas do Jogo/Z3F1.unity");
		} else {

			SceneManager.LoadScene ("Z3F1");
		}
	}

	// LOAD BOSSES //

	[MenuItem("Bosses/Tatuzão &F1", false, 1)] 
	private static void NewMenuOptionA() {

		if (!EditorApplication.isPlaying) {
			
			EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo ();
			EditorSceneManager.OpenScene ("Assets/Scenes/Cenas do Jogo/Z1F1 Boss.unity");
		} else {

			SceneManager.LoadScene ("Z1F1 Boss");
		}
	}

	[MenuItem("Bosses/Gorilão &F2", false, 2)] 
	private static void NewMenuOptionB() {

		if (!EditorApplication.isPlaying) {

			EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo ();
			EditorSceneManager.OpenScene ("Assets/Scenes/Cenas do Jogo/Z1F2 Boss.unity");
		} else {

			SceneManager.LoadScene ("Z1F2 Boss");
		}
	}

	[MenuItem("Bosses/Serradorzão &F3", false, 3)] 
	private static void NewMenuOptionC() {

		if (!EditorApplication.isPlaying) {

			EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo ();
			EditorSceneManager.OpenScene ("Assets/Scenes/Cenas do Jogo/Z1F3 Boss.unity");
		} else {

			SceneManager.LoadScene ("Z1F3 Boss");
		}
	}

	[MenuItem("Bosses/Abutrão &F4", false, 51)] 
	private static void NewMenuOptionD() {

		if (!EditorApplication.isPlaying) {

			EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo ();
			EditorSceneManager.OpenScene ("Assets/Scenes/Cenas do Jogo/Z2F1 Boss.unity");
		} else {

			SceneManager.LoadScene ("Z2F1 Boss");
		}
	}

	[MenuItem("Bosses/Stella's Tears &F5", false, 52)] 
	private static void NewMenuOptionE() {

		if (!EditorApplication.isPlaying) {

			EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo ();
			EditorSceneManager.OpenScene ("Assets/Scenes/Cenas do Jogo/Z2F2 Boss.unity");
		} else {

			SceneManager.LoadScene ("Z2F2 Boss");
		}
	}

	[MenuItem("Bosses/Lixoso &F6", false, 53)] 
	private static void NewMenuOptionF() {

		if (!EditorApplication.isPlaying) {

			EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo ();
			EditorSceneManager.OpenScene ("Assets/Scenes/Cenas do Jogo/Z2F3 Boss.unity");
		} else {

			SceneManager.LoadScene ("Z2F3 Boss");
		}
	}

	[MenuItem("Bosses/Traficante &F7", false, 101)]
	private static void NewMenuOptionG() {

		if (!EditorApplication.isPlaying) {

			EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo ();
			EditorSceneManager.OpenScene ("Assets/Scenes/Cenas do Jogo/Z3F1 Boss.unity");
		} else {

			SceneManager.LoadScene("Z3F1 Boss");
		}
	}
}
