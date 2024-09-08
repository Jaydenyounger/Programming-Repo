/*
 * CS-2123-01(24/FA): Data Structures
 * INPLEMENT THE FOllOWING METHODS:
 * 	1.) public static HashMap<String, String> gs_block(String[] men, String[] women, HashMap<String, String[]> pref, String[][] blocked)
 * 	2.) public static HashMap<String, String> gs_tie(String[] men, String[] women, HashMap<String, String[][]> preftie)
 */
import java.util.*;


public class GS {

	public static void main(String[] args) {
		String[] menlist = {"xavier", "yancey", "zeus"};
		String[] womenlist = {"amy", "bertha", "clare"};
		
		HashMap<String, String[]> thepref = new HashMap<>(); //for standard GS algorithm input
		String[][] names = {
				{"amy", "bertha", "clare"}, //xavier
				{"bertha", "amy", "clare"}, //yancey
				{"amy", "bertha", "clare"}, //zeus
				{"yancey", "xavier", "zeus"}, //amy
				{"xavier", "yancey", "zeus"}, //bertha
				{"xavier", "yancey", "zeus"} //clare
		};
		thepref.put("xavier", names[0]);
		thepref.put("yancey", names[1]);
		thepref.put("zeus", names[2]);
		thepref.put("amy", names[3]);
		thepref.put("bertha", names[4]);
		thepref.put("clare", names[5]);
		
		String[][] blocked = { //for blocked GS algorithm
				{"xavier", "clare"}, {"zeus", "clare"}, {"zeus", "amy"}
		};
		
		HashMap<String, String[][]> thepreftie = new HashMap<>(); //for GS algorithm dealing with ties
		String[][][] names2 = {
				{{"bertha"}, {"amy"}, {"clare"}},
				{{"amy", "bertha"}, {"clare"}},
				{{"amy"}, {"bertha", "clare"}},
				{{"zeus", "xavier", "yancey"}},
				{{"zeus"}, {"xavier"}, {"yancey"}},
				{{"xavier", "yancey"}, {"zeus"}}
		};
		thepreftie.put("xavier", names2[0]);
		thepreftie.put("yancey", names2[1]);
		thepreftie.put("zeus", names2[2]);
		thepreftie.put("amy", names2[3]);
		thepreftie.put("bertha", names2[4]);
		thepreftie.put("clare", names2[5]);
		
		HashMap<String, String> match = gs(menlist, womenlist, thepref);
		System.out.print("{ ");
		for(String w: match.keySet()) {
			System.out.printf("%s: %s, ", w, match.get(w));
		}
		System.out.println("}");
		
		
		HashMap<String, String> match_block = gs_block(menlist, womenlist, thepref, blocked);
		System.out.print("{ ");
		for(String w: match_block.keySet()) {
			System.out.printf("%s: %s, ", w, match_block.get(w));
		}
		System.out.println("}");
		
		HashMap<String, String> match_tie = gs_tie(menlist, womenlist, thepreftie);
		System.out.print("{ ");
		for(String w: match_tie.keySet()) {
			System.out.printf("%s: %s, ", w, match_tie.get(w));
		}
		System.out.println("}");
	}
	
	/*
	 * Original Gale-Shapley Algorithm
	 * Inputs: men (array of men's names)
	 *         women (array of women's names)
	 *         pref (dictionary of preferences mapping names to list of preferred names in sorted order
	 * Output: a Dictionary (HashMap) of stable matches
	 */
	public static HashMap<String, String> gs(String[] men, String[] women, 
			HashMap<String, String[]> pref) {
		//Dictionary (HashMap) to store results
		//first String indicates the woman (key)
		//second String indicates the matched man (value)
		//Please study the use of Java HashMap class 
		HashMap<String, String> S = new HashMap<>(); 
		
		// build the rank dictionary
		HashMap<String, HashMap<String, Integer>> rank = new HashMap<>();
		for(String w: women) {
			HashMap<String, Integer> mrank = new HashMap<>();
			int i = 1;
			for(String m: pref.get(w)) {
				mrank.put(m, i);
				i++;
			}
			rank.put(w, mrank);
		}
		
		// create a pointer to the next woman to propose
		HashMap<String, Integer> prefptr = new HashMap<>();
		for(String m: men) {
			prefptr.put(m, 0); //all starting from 0
		}
		
		// create a freemen list as we did in class
		ArrayList<String> freemen = new ArrayList<String>();
		for(String m: men) {
			freemen.add(m);
		}

		
		//algorithm start here
		while(freemen.size()>0) {
			String m = freemen.remove(0); //always remove from the front end
			int currentPtr = prefptr.get(m);
			String w = pref.get(m)[currentPtr];
			prefptr.put(m, currentPtr+1);
			if(!S.containsKey(w)) S.put(w, m);
			else {
				String mprime = S.get(w);
				if(rank.get(w).get(m)<rank.get(w).get(mprime)) {
					S.put(w, m);
					freemen.add(mprime); //put the previous man back to freemen list
				}
				else {
					freemen.add(m); //put the current man back to freemen list
				}
			}
		}
		
		return S;
	}
	
	/*
	 * Modified Gale-Shapley Algorithm to handle unacceptable matches
	 * Inputs: men (array of men's names)
	 *         women (array of women's names)
	 *         pref (dictionary of preferences mapping names to list of preferred names in sorted order
	 *         blocked (2D array of blocked pairs)
	 * Output: a Dictionary (HashMap) of stable matches        
	 */
	public static HashMap<String, String> gs_block(String[] men, String[] women, HashMap<String, String[]> pref, String[][] blocked) {
    	HashMap<String, String> S = new HashMap<>();
        HashMap<String, HashMap<String, Integer>> rank = new HashMap<>();
            
		// builds something like a ranking dictionary for each women based on their preferance list from the GS algorithm.
            for (String w : women)/* Iterates over the women array*/ {
                HashMap<String, Integer> mrank = new HashMap<>(); // Initilialize the mrank value, this stores the rank for each man.
                int i = 1;
                    for (String m : pref.get(w)) /*Iterates over each man in the preferance list*/ {
                        mrank.put(m, i);
                        i++;
                    }
                rank.put(w, mrank); // stores the ranking in the rank map
            }
        
		// Creates a pointer to the next women.
        HashMap<String, Integer> prefP = new HashMap<>();
		    // iterates over each man and for each man prefP is updated
			for (String m : men) {
                prefP.put(m, 0);// sets the pointer to each man to zero.
            }

		// Add blocked pairs to a list
        HashSet<String> blockedPairs = new HashSet<>();// Initilialize our value for blocked pairs
            // iterates over the blocked array.
			for (String[] pair : blocked) { 
                blockedPairs.add(pair[0] + "-" + pair[1]); // Add the blocked pair to the blockedPairs value.
            }

        // creates a list of men who are unmatched. 
        ArrayList<String> freemen = new ArrayList<>();
            //adds every man to the freemen list
			for (String m : men) { 
                freemen.add(m);
            }

			// continues as long there is a free men in the list.
       		while (freemen.size() > 0) { 
				String m = freemen.remove(0); // removes and retrieves the first man from the list.
            	int currentPtr = prefP.get(m); // gets the current proposal pointer from the man.        
       		// check if man hass proposes to all the women in their list.
			if (currentPtr >= pref.get(m).length) {
            	continue;
        	}
            
        String w = pref.get(m)[currentPtr]; // get women that the man will propose to.
        prefP.put(m, currentPtr + 1); // updates the pointer for man to propose to the next woman on his list.
            // check is we have a set of blocked pairs.
			if (blockedPairs.contains(m + "-" + w)) {
                freemen.add(m); 
                continue;
            }

            // check if women is matched with someone.
            if (!S.containsKey(w)) {
                S.put(w, m); // puts woman the matching map var S.

            } else {
                String mprime = S.get(w); // gets the the current partner the matching map.
            
			// compares the preferances of the a women with 2 men m and mprime. m is the current man currently proposing to her and Mprime is the man she is currently matched with.
			if (rank.get(w).get(m) < rank.get(w).get(mprime)) {
                S.put(w, m);
                freemen.add(mprime); 
            } else {
                freemen.add(m); 
                }
            }
        }  
    return S;
	}
	
	/*
	 * Modified Gale-Shapley Algorithm to handle ties
	 * Inputs: men (array of men's names)
	 *         women (array of women's names)
	 *         preftie (dictionary of preferences mapping names to list of preferred names in sorted order
	 * Output: a Dictionary (HashMap) of stable matches
	 */
	public static HashMap<String, String> gs_tie(String[] men, String[] women, HashMap<String, String[][]> preftie) {
        HashMap<String, String> S = new HashMap<>();
        HashMap<String, HashMap<String, Integer>> rank = new HashMap<>();
		
		// similar to the gs_block, ranking dictionary for each women based on their preferance list from the GS algorithm.
        for (String w : women) {
            HashMap<String, Integer> mrank = new HashMap<>();
            int i = 1;
			// loops over the tied groups.
            for (String[] tieGroup : preftie.get(w)) {
				// Ranks the Men in the tie groups
                for (String m : tieGroup) {
                    mrank.put(m, i);
                }
                        i++;
            }
            rank.put(w, mrank); // Stroes to the ranking map.
        }
		
                
        HashMap<String, Integer> prefptr = new HashMap<>(); // create a Preferance pointer
			// iterates over each man in the list.
        	for (String m : men) {
                prefptr.put(m, 0); // puts them in the prefptr hash map and sets the pointer value to 0.
            }
            
        // creates a list of men who are unmatched.       
        ArrayList<String> freemen = new ArrayList<>();
			//adds every man to the freemen list
            for (String m : men) {
                freemen.add(m);
            }
            
        //  perform the matching process.    
        while (freemen.size() > 0) { // continues as long there is a free man
            String m = freemen.remove(0); //removes  the next free man.
            int currentPtr = prefptr.get(m); // gets the current pointer.
            String[] currentTieGroup = preftie.get(m)[currentPtr]; // gets the current tie group.
            String w = currentTieGroup[0]; // get women that the man will propose to.
            prefptr.put(m, currentPtr + 1); // updates the pointer

				// if woman is matched with man, put them into the S map.
            	if (!S.containsKey(w)) {
                    S.put(w, m);
                } else {
                    String mprime = S.get(w); // retrieves the mprime
				// compares the m and prime to compare their rank.
                if (rank.get(w).get(m) < rank.get(w).get(mprime)) {
                    S.put(w, m); // if m < mprime, put m and w into the S list.
                    freemen.add(mprime);  // add mprime to the freemen list.
                } else {
                    freemen.add(m); // if mprime < m, and m to the freemen list.
                }
            }
        }    
    return S;
	}

}
